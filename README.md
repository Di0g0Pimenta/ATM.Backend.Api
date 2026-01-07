# ATM.Backend.Api (Sistema Multibanco com Autenticação)

Este projeto implementa o backend de um sistema ATM (Multibanco) utilizando .NET 8, Entity Framework Core e SQL Server. O projeto foi estruturado seguindo boas práticas de POO e arquitetura limpa, e está dockerizado para facilidade de execução.

## 🚀 Como Executar o Projeto

### Pré-requisitos
*   **Recomendado**: Docker Desktop instalado.
*   **Alternativa (Local)**: .NET 8 SDK e SQL Server LocalDB instalados.

### Opção 1: Executando com Docker (Recomendado)
Esta opção sobe a API e o Banco de Dados automaticamente em containers isolados.

**Primeira vez e subsequentes:**
Abra o terminal na pasta raiz do projeto e execute:
```bash
docker-compose up --build -d
```

*   **Acesse o Swagger UI**: [http://localhost:8080/swagger](http://localhost:8080/swagger)
*   **Parar execução**: `docker-compose down`

### Opção 2: Executando Localmente (Visual Studio)
1.  Abra o arquivo `ATM.Backend.Api.sln` no Visual Studio.
2.  Altere a Connection String em `appsettings.json` para apontar para seu SQL Server local.
3.  Execute o comando `update-database` no Package Manager Console para criar o banco.
4.  Pressione `F5` para iniciar o projeto.

---

## ✅ Checklist de Requisitos - Projeto Final POO
Abaixo está o status atual do desenvolvimento em relação aos objetivos do projeto final.

### 1. Objetivos Gerais e POO
- [x] **Aplicação dos princípios de POO**: Projeto refatorado com herança (`Model`), encapsulamento e abstração (`IRepository`).
- [x] **Classes Base**: `Account`, `Client`, `Card`, `Bank`, `Movement` 100% implementadas.
- [x] **Persistência**: Tabelas criadas e mapeadas via EF Core.

### 2. Requisitos Funcionais
- [x] **Consultar Saldo**: Campo `Balance` funcional na classe `Account`.
- [x] **Levantar / Depositar Dinheiro**: Métodos `Debit` e `Deposit` com lógica de validação.
- [x] **Registo de Movimentos**: Entidade `Movement` pronta para histórico.
- [x] **Gestão de Clientes**: CRUD completo via API REST.

### 3. Segurança e Infraestrutura
- [x] **JWT Auth**: Autenticação via Token JWT funcional.
- [x] **Proteção de Endpoints**: Acesso restrito via `[Authorize]`.
- [x] **Dockerização**: Solução completa com App e SQL Server em containers.
- [ ] **Hash de Senhas**: Senhas em texto plano (Próximo passo sugerido).

---

## 📚 Documentação da API

A API utiliza o prefixo base `/multibanco`.

### 🔐 Autenticação (`/auth`)
*   `POST /auth/login`: Realiza o login.
    *   **Request Body**: `{ "email": "admin@email.com", "password": "123" }`
    *   **Response**: `{ "client": {...}, "token": "..." }`

### 👤 Clientes (`/client`)
*   `POST /client`: **Registo Público**. Cria um novo cliente.
*   `GET /client`: **Protegido**. Lista todos os clientes.
*   `GET /client/{id}`: **Protegido**. Detalhes de um cliente.
*   `PUT /client/{id}`: **Protegido**. Atualiza dados.
*   `DELETE /client/{id}`: **Protegido**. Remove cliente.

> [!TIP]
> No Swagger, use o botão **Authorize** e insira o valor: `Bearer <seu_token>`.

---

## 📂 Estrutura do Projeto

Abaixo segue uma explicação detalhada da organização das pastas e arquivos principais do projeto `ATM.Backend.Api`.

### 📁 Diretórios Principais

*   **`Controllers/`**: Contém os controladores da API, responsáveis por receber as requisições HTTP e retornar as respostas.
    *   **`Local/`**: Controladores para operações locais (simulação de terminal).
    *   **`Rest/`**: Controladores para a API RESTful padrão.
*   **`Data/`**: Camada de acesso a dados.
    *   **`AppDbContext.cs`**: Contexto do Entity Framework Core que gerencia a conexão com o banco de dados e mapeia as entidades para tabelas.
*   **`Models/`**: Define as entidades de domínio do sistema.
    *   **`Account.cs`**: Representa uma conta bancária.
    *   **`Card.cs`**: Representa um cartão associado a uma conta.
    *   **`Client.cs`**: Representa um cliente do banco.
*   **`Repositories/`**: Implementação do padrão Repository para abstrair a lógica de acesso a dados.
    *   **`GenericRepository.cs`**: Implementação genérica de operações CRUD.
    *   **`IRepository.cs`**: Interface genérica para os repositórios.
*   **`Migrations/`**: Arquivos gerados pelo Entity Framework para versionamento e evolução do esquema do banco de dados.

### 📄 Arquivos Importantes

*   **`Program.cs`**: O ponto de entrada da aplicação. Configura a injeção de dependência, o pipeline de requisição HTTP, a conexão com o banco de dados e o Swagger.
*   **`appsettings.json`**: Arquivo de configuração da aplicação (ex: connection strings, níveis de log).
*   **`Dockerfile`**: Instruções para criar a imagem Docker da aplicação, permitindo que ela rode em um container isolado.
*   **`docker-compose.yml`**: (Na raiz da solução) Orquestra os containers da aplicação e do banco de dados SQL Server para subirem juntos.
