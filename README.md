# 🏗️ Sistema de Monitoramento de Equipamentos Pesados

> API REST para gerenciamento de equipamentos de mineração - Desafio Final Bootcamp Deloitte

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Funcionalidades](#funcionalidades)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Pré-requisitos](#pré-requisitos)
- [Instalação e Execução](#instalação-e-execução)
- [Endpoints da API](#endpoints-da-api)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Banco de Dados](#banco-de-dados)

## 📖 Sobre o Projeto

Sistema para gerenciamento completo de equipamentos pesados em operações de mineração. Permite controle de frota incluindo caminhões, escavadeiras, perfuratrizes, carregadeiras e tratores com monitoramento de status operacional, horímetro e localização.

## ✨ Funcionalidades

- ✅ Cadastro, consulta, atualização e exclusão de equipamentos (CRUD completo)
- 🔍 Listagem paginada com filtros por tipo, status e código
- 📊 Controle de horímetro e status operacional
- 📍 Rastreamento de localização de equipamentos
- 📅 Registro de data de aquisição
- 🚀 API REST com padrão de resposta estruturado

## 🏛️ Arquitetura

O projeto implementa **Clean Architecture** com separação em camadas:

```
┌─────────────────────────────────────┐
│      DesafioFinal.Api               │  → Endpoints, Configuração
├─────────────────────────────────────┤
│    DesafioFinal.Application         │  → DTOs, Services, Interfaces
├─────────────────────────────────────┤
│   DesafioFinal.Infrastructure       │  → Repositories, EF Core, Migrations
├─────────────────────────────────────┤
│      DesafioFinal.Domain            │  → Entities, Enums, Regras de Negócio
└─────────────────────────────────────┘
```

**Camadas:**

- **Domain**: Entidades (`Equipamento`) e enums (`TipoEquipamento`, `StatusOperacional`)
- **Application**: DTOs, serviços de aplicação e interfaces de repositórios
- **Infrastructure**: Implementação de repositórios, contexto EF Core e migrations
- **Api**: Endpoints Minimal API e configuração da aplicação

## 🛠️ Tecnologias

- ![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet) **ASP.NET Core Minimal APIs**
- ![EF Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4) **Entity Framework Core**
- ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?logo=postgresql) **Banco de dados relacional**
- ![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker) **Containerização**
- **Npgsql** - Driver PostgreSQL para .NET
- **OpenAPI/Swagger** - Documentação automática

## 📦 Pré-requisitos

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) e [Docker Compose](https://docs.docker.com/compose/install/)
- [Insomnia](https://insomnia.rest/) ou [Postman](https://www.postman.com/) (opcional, para testes)
- [DBeaver](https://dbeaver.io/) ou [pgAdmin](https://www.pgadmin.org/) (opcional, para gerenciar o banco)

## 🚀 Instalação e Execução

### Usando Docker Compose (Recomendado)

1. **Clone o repositório**

   ```bash
   git clone https://github.com/seu-usuario/desafio-final-deloitte.git
   cd desafio-final-deloitte
   ```

2. **Inicie os containers**

   ```bash
   docker-compose up -d
   ```

3. **Acesse a aplicação**
   - API: http://localhost:8080
   - OpenAPI: http://localhost:8080/openapi/v1.json

4. **Credenciais do PostgreSQL**

   ```
   Host:     localhost
   Port:     5432
   Database: desafiofinal
   User:     postgres
   Password: postgres
   ```

5. **Parar os containers**
   ```bash
   docker-compose down
   ```

### Rodando Localmente (Sem Docker)

1. **Configure o PostgreSQL local**
   - Crie um banco de dados chamado `desafiofinal`

2. **Atualize a connection string**

   Edite `src/DesafioFinal.Api/appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=desafiofinal;Username=postgres;Password=sua-senha"
     }
   }
   ```

3. **Execute a aplicação**

   ```bash
   cd src/DesafioFinal.Api
   dotnet restore
   dotnet run
   ```

4. **Acesse a aplicação**
   - API: http://localhost:5084

> **Nota:** As migrations são aplicadas automaticamente ao iniciar a aplicação.

## 🔌 Endpoints da API

Base URL: `http://localhost:8080/api` (Docker) ou `http://localhost:5084/api` (Local)

### Equipamentos

| Método   | Endpoint             | Descrição                      | Autenticação |
| -------- | -------------------- | ------------------------------ | ------------ |
| `POST`   | `/equipamentos`      | Criar novo equipamento         | Não          |
| `GET`    | `/equipamentos`      | Listar equipamentos (paginado) | Não          |
| `GET`    | `/equipamentos/{id}` | Buscar equipamento por ID      | Não          |
| `PUT`    | `/equipamentos/{id}` | Atualizar equipamento          | Não          |
| `DELETE` | `/equipamentos/{id}` | Deletar equipamento            | Não          |

### Exemplos de Requisições

#### 1. Criar Equipamento

```http
POST /api/equipamentos
Content-Type: application/json

{
  "codigo": "CAM-001",
  "tipo": "Caminhao",
  "modelo": "Volvo FMX 540",
  "horimetro": 1250.5,
  "statusOperacional": "Operacional",
  "dataAquisicao": "2024-01-15",
  "localizacaoAtual": "Mina Norte - Setor A"
}
```

**Resposta (201 Created):**

```json
{
  "id": 1,
  "codigo": "CAM-001",
  "tipo": "Caminhao",
  "modelo": "Volvo FMX 540",
  "horimetro": 1250.5,
  "statusOperacional": "Operacional",
  "dataAquisicao": "2024-01-15",
  "localizacaoAtual": "Mina Norte - Setor A"
}
```

#### 2. Listar Equipamentos (Paginado e Filtrado)

```http
GET /api/equipamentos?page=1&pageSize=10&tipo=Caminhao&status=Operacional
```

**Resposta (200 OK):**

```json
{
  "items": [
    {
      "id": 1,
      "codigo": "CAM-001",
      "tipo": "Caminhao",
      "modelo": "Volvo FMX 540",
      "horimetro": 1250.5,
      "statusOperacional": "Operacional",
      "dataAquisicao": "2024-01-15",
      "localizacaoAtual": "Mina Norte - Setor A"
    }
  ],
  "page": 1,
  "pageSize": 10,
  "totalCount": 1,
  "totalPages": 1
}
```

#### 3. Buscar por ID

```http
GET /api/equipamentos/1
```

#### 4. Atualizar Equipamento

```http
PUT /api/equipamentos/1
Content-Type: application/json

{
  "codigo": "CAM-001",
  "tipo": "Caminhao",
  "modelo": "Volvo FMX 540",
  "horimetro": 1350.5,
  "statusOperacional": "EmManutencao",
  "dataAquisicao": "2024-01-15",
  "localizacaoAtual": "Oficina Central"
}
```

**Resposta (204 No Content)**

#### 5. Deletar Equipamento

```http
DELETE /api/equipamentos/1
```

**Resposta (204 No Content)**

### Enums Disponíveis

**TipoEquipamento:**

- `Caminhao`
- `Escavadeira`
- `Perfuratriz`
- `Carregadeira`
- `Trator`

**StatusOperacional:**

- `Operacional`
- `EmManutencao`
- `Parado`

## 📁 Estrutura do Projeto

```
Desafio-final-Deloitte-bootcamp/
├── src/
│   ├── DesafioFinal.Api/              # Camada de apresentação
│   │   ├── Program.cs                 # Endpoints Minimal API
│   │   ├── appsettings.json           # Configurações
│   │   └── Dockerfile                 # Container da API
│   │
│   ├── DesafioFinal.Application/      # Camada de aplicação
│   │   ├── Dtos/                      # Data Transfer Objects
│   │   ├── Services/                  # Lógica de aplicação
│   │   ├── Repositories/              # Interfaces de repositórios
│   │   └── Common/                    # PagedResult, ServiceResult
│   │
│   ├── DesafioFinal.Domain/           # Camada de domínio
│   │   ├── Entities/                  # Entidades de negócio
│   │   │   └── Equipamento.cs
│   │   └── Enums/                     # Enumerações
│   │       ├── TipoEquipamento.cs
│   │       └── StatusOperacional.cs
│   │
│   └── DesafioFinal.Infrastructure/   # Camada de infraestrutura
│       ├── Persistence/               # DbContext
│       ├── Repositories/              # Implementações de repositórios
│       └── Migrations/                # Migrations EF Core
│
├── insomnia/                          # Collection para testes
│   └── equipamentos-collection.json
│
├── database-schema.sql                # Schema do banco
├── database-seed.sql                  # Dados iniciais
├── database-queries.sql               # Queries úteis
├── docker-compose.yml                 # Orquestração de containers
└── README.md                          # Este arquivo
```

## 🗄️ Banco de Dados

### Schema Principal

**Tabela: equipamentos**

| Coluna               | Tipo          | Descrição                     |
| -------------------- | ------------- | ----------------------------- |
| `id`                 | INT (PK)      | Identificador único           |
| `codigo`             | VARCHAR(100)  | Código do equipamento (único) |
| `tipo`               | INT           | Tipo do equipamento (enum)    |
| `modelo`             | VARCHAR(200)  | Modelo do equipamento         |
| `horimetro`          | DECIMAL(18,2) | Horas de uso                  |
| `status_operacional` | INT           | Status atual (enum)           |
| `data_aquisicao`     | DATE          | Data de aquisição             |
| `localizacao_atual`  | VARCHAR(300)  | Localização atual             |

### Scripts Disponíveis

- `database-schema.sql` - Estrutura completa do banco
- `database-seed.sql` - Dados de exemplo para testes
- `database-queries.sql` - Queries úteis para consultas

### Acessar o Banco

**Via DBeaver/pgAdmin:**

```
Host:     localhost
Port:     5432
Database: desafiofinal
User:     postgres
Password: postgres
```

**Via Docker:**

```bash
docker exec -it desafiofinal-db psql -U postgres -d desafiofinal
```

## 📝 Collection Insomnia

Uma collection completa com todos os endpoints está disponível em:

```
insomnia/equipamentos-collection.json
```

**Para importar:**

1. Abra o Insomnia
2. Application → Preferences → Data → Import Data
3. Selecione o arquivo `equipamentos-collection.json`

---

**Desenvolvido como Desafio Final do Bootcamp Deloitte** 🚀
