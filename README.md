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
