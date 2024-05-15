## 📱 Projeto
Este projeto é uma API de Ecommerce desenvolvida em C# usando .NET 8.0 Ele fornece um conjunto completo de recursos para gerenciar pedidos, clientes e produtos. A API é projetada para lidar com todas as operações CRUD (Create, Read, Update, Delete) para essas entidades e tambem para adicionar tabelas do excel e ler o conteudo do arquivo e adiconar no banco de dados.
## ⚙️ Tecnologias
* C# 12: A versão mais recente do C# é usada para o desenvolvimento desta API.  
* .NET 8.0:  API é construída no framework .NET 8.0.  
* Entity Framework Core: Entity Framework Core é usado para acesso a dados.  
* InMemory: InMemory é usado como banco de dados para este projeto.
* EPPlus: EPPlus é usado para manipular arquivos Excel.
* Swagger: Swagger é usado para documentar a API.

## 🧪 Como testar o projeto com Swagger

##### Link para testar
`http://localhost:5124/swagger/index.html`


## 🧪 Como testar o projeto na sua máquina

##### Instalação
- Certifique-se de ter o .NET SDK instalado em sua máquina. Você pode baixar o SDK em https://dotnet.microsoft.com/download.

- A versão do .NET que esta nesse Projeto é o .NET 8.0

- O comando específico para instalar o .NET 8.0 pelo terminal depende do sistema operacional que você está usando.

##### No Windows:
- Com PowerShell:
```bash
  Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
  Install-Module dotnet
  Import-Module dotnet
  Install-Package dotnet-sdk-8.0
  ```
- Com Prompt de Comando:

```bash  
dotnet-install.ps1 -InstallSDK 8.0
```
##### No macOS:
```bash  
brew install dotnet/core/dotnet-sdk8
```
##### No Linux:

- Com Ubuntu:
```bash  
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install dotnet-sdk-8.0
```
- Com outras distros:
- Consulte a documentação do .NET para obter instruções específicas para sua distro: https://docs.microsoft.com/pt-br/dotnet/core/install/linux.

##### Observações:

- Certifique-se de ter um gerenciador de pacotes instalado em sua máquina.
- O comando dotnet-install.ps1 só está disponível no Windows.
- O comando brew só está disponível no macOS.
- Os comandos para Linux podem variar dependendo da sua distro.

##### Clonar o repositorio
```bash
git clone https://github.com/Mizaeldouglas/Teste-CSharp-ReactJS.git
cd Teste-CSharp-ReactJS/salles-dashboard/
```


##### Iniciar o projeto na sua máquina

```bash
dotnet restore
```
```bash
dotnet clean
```
```bash
dotnet build
```
```bash
dotnet run urls=http://localhost:5173
```
* PS: A porta padrão é 5173, caso queira mudar a porta, altere o valor no arquivo launchSettings.json e no configutation/ConfigurationCors.cs