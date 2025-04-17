# Gerenciador de Funcionários - API .NET Core

Este projeto é uma API RESTful construída com .NET Core e Entity Framework Core para gerenciamento de departamentos e funcionários, que serve como backend para o projeto Angular de mesmo nome.

## Visão Geral do Projeto

A API fornece endpoints para:
- Gerenciamento de departamentos (CRUD)
- Gerenciamento de funcionários (CRUD)
- Upload de fotos de funcionários
- Consulta de funcionários por departamento

## Tecnologias Utilizadas

- .NET Core
- Entity Framework Core
- SQL Server
- Swagger (para documentação da API - se estiver configurado)

## Configuração do Ambiente

### Pré-requisitos

- .NET SDK (versão compatível com o projeto)
- SQL Server (local ou remoto)
- IDE de sua preferência (Visual Studio, VS Code, etc.)

### Configuração do Banco de Dados

1. Atualize a string de conexão no arquivo `FuncionarioContext.cs` para apontar para seu SQL Server:
```csharp
optionsBuilder.UseSqlServer("Sua_String_De_Conexão_Aqui");
```

2. Execute as migrações para criar o banco de dados:
```bash
dotnet ef database update
```

## Executando o Projeto

1. Restaure as dependências:
```bash
dotnet restore
```

2. Execute o projeto:
```bash
dotnet run
```

A API estará disponível em `https://localhost:5001` (ou porta configurada).

## Estrutura do Projeto

- `Controllers/`: Contém os controladores da API
  - `DepartamentoController.cs`: Endpoints para gerenciamento de departamentos
  - `FuncionarioController.cs`: Endpoints para gerenciamento de funcionários
- `Data/`: Contém o contexto do banco de dados
  - `FuncionarioContext.cs`: Configuração do DbContext
- `Models/`: Contém as entidades do sistema
  - `Departamento.cs`: Modelo de departamento
  - `Funcionario.cs`: Modelo de funcionário
- `Migrations/`: Contém as migrações do Entity Framework Core

## Endpoints Principais

### Departamentos
- `GET /api/Departamento`: Lista todos os departamentos
- `GET /api/Departamento/{id}`: Obtém um departamento por ID
- `POST /api/Departamento`: Cria um novo departamento
- `PUT /api/Departamento/{id}`: Atualiza um departamento
- `DELETE /api/Departamento/{id}`: Remove um departamento

### Funcionários
- `GET /api/Funcionario`: Lista todos os funcionários
- `GET /api/Funcionario/{id}`: Obtém um funcionário por ID
- `POST /api/Funcionario`: Cria um novo funcionário
- `PUT /api/Funcionario/{id}`: Atualiza um funcionário
- `DELETE /api/Funcionario/{id}`: Remove um funcionário
- `GET /api/Funcionario/departamento/{departamentoId}`: Lista funcionários por departamento
- `POST /api/Funcionario/upload`: Upload de foto para funcionário

## Funcionalidades Especiais

- **Reset de IDs**: Quando as tabelas estão vazias, os IDs são resetados para 0
- **Upload de Fotos**: As fotos dos funcionários são salvas no sistema de arquivos e referenciadas no banco de dados
- **Relação Departamento-Funcionário**: Cada funcionário está associado a um departamento com relação 1:N

## Integração com o Frontend Angular

Esta API foi projetada para trabalhar em conjunto com o frontend Angular disponível no repositório correspondente. Certifique-se de configurar a URL base da API no ambiente Angular para apontar para esta API.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests com melhorias ou correções.
