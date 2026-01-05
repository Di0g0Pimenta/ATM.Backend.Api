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

### 1. Objetivos Gerais
- [x] **Aplicação dos princípios de POO**: Projeto refatorado com herança (`Model`), encapsulamento (propriedades `private set` onde aplicável) e abstração (`IRepository`).
- [ ] **Persistência dos movimentos**: O banco salva Clientes, Contas e Cartões, mas a tabela de Movimentos ainda não foi criada.

### 2. Requisitos Funcionais
#### 2.1 Consultar Saldo
- [x] **Estrutura de Dados**: Campo `Balance` existe na classe `Account`.

#### 2.2 Levantar Dinheiro (Pick-up Money)
- [x] **Lógica Básica**: Métodos `Debit` na classe `Account` verificam saldo.
- [ ] **Registo de Movimento**: Falta criar entidade `Movement` e salvar o registo da operação no banco.

#### 2.3 Depositar Dinheiro (Store Money)
- [x] **Lógica Básica**: Métodos `Deposit` na classe `Account`.
- [ ] **Registo de Movimento**: Falta criar entidade `Movement` e registrar.

#### 2.4 Listagem de Movimentos
- [ ] **Histórico**: Falta implementar a entidade `Movement` e o endpoint/visualização para listar o histórico.

### 3. Requisitos Técnicos
#### 3.1 POO
- [x] **Classes Base**: `Conta` (Account), `Cliente` (Client), `Cartão` (Card) implementadas.
- [ ] **Classes Faltantes**: `Movimento`, `Banco` (se for multi-banco real).
- [ ] **Polimorfismo nas Operações**: Implementar classes derivadas para operações (ex: `Levantamento : Operacao`) para cumprir o requisito de polimorfismo.

#### 3.2 Banco de Dados
- [x] **SQL Server**: Configurado e rodando (via Docker ou Local).
- [x] **Eficiência**: Uso de Entity Framework e Repository Pattern.

#### 3.3 Segurança
- [ ] **Hash de Senhas**: As senhas ainda estão em texto plano. Necessário implementar hashing (ex: BCrypt).
- [x] **Validações**: Validações básicas de modelo implementadas.

### 4. Funcionalidades Extras (Diferenciadores)
- [x] **Dockerização**: Projeto totalmente containerizado (Item de "Use of AI Tools" / "Knowledge Expansion").
- [ ] **Transferências/Pagamentos**: A implementar.
- [ ] **Dashboard**: A implementar no frontend.

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
