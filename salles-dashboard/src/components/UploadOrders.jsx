/* eslint-disable react/no-unescaped-entities */
import { useState, useEffect } from "react";
import axios from "axios";

const UploadOrders = () => {
  const [file, setFile] = useState(null);
  const [files, setFiles] = useState([]);
  const [isButtonDisabled, setIsButtonDisabled] = useState(false);

  useEffect(() => {
    axios
      .get("http://localhost:5124/api/Import/GetAllFiles")
      .then((response) => setFiles(response.data))
      .catch((error) => console.error(error));
  }, []);

  const onFileChange = (e) => {
    const file = e.target.files[0];
    const fileExtension = file.name.split(".").pop().toLowerCase();
    const isExcelFile = fileExtension === "xls" || fileExtension === "xlsx";

    if (!isExcelFile) {
      alert("Por favor, envie apenas arquivos Excel (.xls, .xlsx)");
      return;
    }

    setFile(file);
  };

  const onFileUpload = () => {
    const formData = new FormData();
    formData.append("file", file);
    axios
      .post("http://localhost:5124/api/Import/UploadExcel", formData)
      .then(() => {
        window.location.reload();
      });
  };

  const onFileImport = () => {
    if (files.length > 0) {
      setIsButtonDisabled(true);
      const lastFile = files[files.length - 1].path;
      axios.post(
        `http://localhost:5124/api/Import?filePath=${encodeURIComponent(
          lastFile
        )}`
      );
    }
  };

  return (
    <div className="mt-3">
      <h1 className="my-3">Importação de Pedidos</h1>
      <p>
        Este sistema permite que você importe pedidos a partir de uma planilha.
        Primeiro, faça o upload da planilha e, em seguida, clique em 'Importar
        Planilhas' para importar os dados da planilha para o sistema.
      </p>
      <p>
        Na tabela de produtos, nós só vendemos produtos específicos que são:
        <strong>Televisão, Celular, Notebook</strong>. Se colocar outro tipo de
        produto, nós vamos <strong>ignorá-los</strong>.
      </p>
      <a
        href="https://docs.google.com/spreadsheets/d/1htc2DHNomvfUtr3pOizMjb0d6X9NuKvlGMw-mkUnaiM/edit#gid=0"
        download
        target="_blank"
      >
        Clique aqui para baixar um exemplo de planilha
      </a>
      <br />
      <hr />
      <br />
      <h1 className="my-3">Exemplo</h1>
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">Documento</th>
            <th scope="col">Razão Social</th>
            <th scope="col">CEP</th>
            <th scope="col">Produto</th>
            <th scope="col">Número do pedido</th>
            <th scope="col">Data</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>123456789</td>
            <td>MyName</td>
            <td>1234567</td>
            <td>Televisão</td>
            <td>1</td>
            <td>20/02/1999</td>
          </tr>
          <tr>
            <td>123456789</td>
            <td>MyName</td>
            <td>1234567</td>
            <td>Celular</td>
            <td>1</td>
            <td>20/02/1999</td>
          </tr>
          <tr>
            <td>123456789</td>
            <td>MyName</td>
            <td>1234567</td>
            <td>Notebook</td>
            <td>1</td>
            <td>20/02/1999</td>
          </tr>
        </tbody>
      </table>
      <br />
      <hr />
      <br />
      <input
        type="file"
        onChange={onFileChange}
        className="form-control my-3"
      />
      <button onClick={onFileUpload} className="btn btn-primary me-2">
        Upload
      </button>
      <button
        onClick={onFileImport}
        disabled={isButtonDisabled}
        className="btn btn-success"
      >
        Importar Panilhas
      </button>
    </div>
  );
};

export default UploadOrders;
