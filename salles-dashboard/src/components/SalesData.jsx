import { useEffect, useState } from "react";
import {
  PieChart,
  Pie,
  Cell,
  Tooltip,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Legend,
} from "recharts";

import axios from "axios";
import { format } from "date-fns";

const SalesData = () => {
  const [data, setData] = useState([]);
  const [valorFinal, setValorFinal] = useState([]);
  const [valorPorRegiao, setValorPorRegiao] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get("http://localhost:5124/api/Order").then((res) => {
      const promises = res.data.map((order) => {
        const freightPromise = axios.get(
          `http://localhost:5124/api/CalcFreight/${order.cpF_CNPJ}`
        );
        const deliveryTimePromise = axios.get(
          `http://localhost:5124/api/CalcDeliveryTime/${order.cpF_CNPJ}`
        );

        return Promise.all([freightPromise, deliveryTimePromise]).then(
          (results) => {
            // Adiciona os dados de frete e tempo de entrega ao pedido
            order.freight = results[0].data;
            order.deliveryTime = results[1].data;
            return order;
          }
        );
      });

      Promise.all(promises).then((orders) => {
        setData(orders);
        setLoading(false);
      });

      axios
        .get(
          "http://localhost:5124/api/CalcFreight/CalculateTotalWithFreightForAllOrders"
        )
        .then((res) => {
          setValorFinal(res.data);
        });

      axios
        .get("http://localhost:5124/api/CalcFreight/CalculateTotalByRegion")
        .then((res) => {
          const data = Object.entries(res.data).map(([region, value]) => ({
            name: `Região: ${region}`,
            value,
          }));
          setValorPorRegiao(data);
        });
    });
  }, []);

  const COLORS = [
    "#0088FE",
    "#00C49F",
    "#FFBB28",
    "#FF8042",
    "#AF19FF",
    "#FF1919",
    "#19FF19",
    "#1919FF",
    "#FF19FF",
    "#19FFFF",
  ];

  const clientNames = data.map((order) => order.name);

  const ProductPrice = data.slice(0, 3).map((order) => ({
    name: order.product.name,
    price: order.product.preco,
  }));

  console.log(ProductPrice);
  const dataValor = valorFinal.map((value, index) => ({
    name: `${clientNames[index]}`,
    value,
  }));

  const dataRegiao = valorPorRegiao.map((value) => ({
    name: `${value.name}`,
    value: value.value,
  }));

  if (loading) {
    return <div>Carregando...</div>;
  }

  return (
    <div className="container">
      <h1 className="mb-3">Dashboard de Vendas</h1>
      <br />
      <hr />
      <br />

      <div className="row">
        <div className="col-md-4">
          <h2>Valor Final</h2>
          <PieChart width={400} height={400}>
            <Pie
              dataKey="value"
              isAnimationActive={false}
              data={dataValor}
              cx={200}
              cy={200}
              outerRadius={100}
              fill="#8884d8"
              label
            >
              {dataValor.map((entry, index) => (
                <Cell
                  key={`cell-${index}`}
                  fill={COLORS[index % COLORS.length]}
                />
              ))}
            </Pie>
            <Tooltip />
          </PieChart>
        </div>

        <div className="col-md-4">
          <h2>Valor por Região</h2>
          <PieChart width={400} height={400}>
            <Pie
              dataKey="value"
              isAnimationActive={false}
              data={dataRegiao}
              cx={200}
              cy={200}
              outerRadius={100}
              fill="#123456"
              label
            >
              {dataRegiao.map((entry, index) => (
                <Cell
                  key={`cell-${index}`}
                  fill={COLORS[index % COLORS.length]}
                />
              ))}
            </Pie>
            <Tooltip />
          </PieChart>
        </div>

        <div className="col-md-4">
          <h2>Preço do Produto</h2>
          <BarChart
            width={400}
            height={300}
            data={ProductPrice}
            margin={{
              top: 5,
              right: 30,
              left: 20,
              bottom: 5,
            }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <Legend />
            <Bar dataKey="price" fill="#8884d8" />
          </BarChart>
        </div>
      </div>

      <h2 className="mb-3">Lista de Pedidos</h2>

      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">Documento</th>
            <th scope="col">Razão Social</th>
            <th scope="col">CEP</th>
            <th scope="col">Produto</th>
            <th scope="col">Número do pedido</th>
            <th scope="col">Data</th>
            <th scope="col">Valor Final</th>
            <th scope="col">Prazo de chegada</th>
          </tr>
        </thead>
        <tbody>
          {data.map((order, index) => (
            <tr key={index}>
              <td>{order.cpF_CNPJ}</td>
              <td>{order.name}</td>
              <td>{order.cep}</td>
              <td>{order.product.name}</td>
              <td>{order.numberOrder}</td>
              <td>{format(new Date(order.data), "dd/MM/yyyy")}</td>
              <td>R$ {valorFinal[index]}</td>
              <td>{order.deliveryTime} dias</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default SalesData;
