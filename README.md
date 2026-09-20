# 💳 Pagamentos App

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://docs.microsoft.com/dotnet/csharp/)
[![MongoDB](https://img.shields.io/badge/MongoDB-4EA94B?style=for-the-badge&logo=mongodb&logoColor=white)](https://www.mongodb.com/)

API RESTful desenvolvida em **.NET 10 (C#)** voltada para a leitura, interpretação e extração inteligente de dados a partir de comprovantes de pagamento (PDF, JPG, etc.), utilizando recursos avançados de **Inteligência Artificial (IA)** para automação de processos de gestão financeira.

---

## 📋 Sumário
- [Sobre o Projeto](#-sobre-o-projeto)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Pré-requisitos](#-pré-requisitos)
- [Configuração](#-configuração)

---

## 📌 Sobre o Projeto

O **Pagamentos App** tem como finalidade simplificar a recepção e o processamento de comprovantes financeiros. A solução recebe os documentos em diversos formatos (PDF, JPG, etc.), realiza a interpretação via IA e extrai automaticamente dados relevantes, tais como:

* 💰 **Valores**
* 📅 **Datas**
* 🔢 **Identificadores de transações**

Após o processamento, os dados são persistidos em um banco de dados **MongoDB** e disponibilizados através de endpoints para consulta e integração com sistemas de gestão financeira.

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem & Framework:** C# / .NET 10 SDK
* **Banco de Dados:** MongoDB
* **Inteligência Artificial:** Serviços de IA para parsing/extração de dados de documentos

---

## ⚙️ Pré-requisitos

* **.NET 10 SDK** para desenvolvimento/publish (opcional se utilizar o binário publicado).
* Instância do **MongoDB** configurada e acessível.
* Chave de API para serviços de IA.

---

## 🔧 Configuração

Configure as variáveis de ambiente necessárias ou edite o arquivo `appsettings.json` na raiz da aplicação com as configurações de conexão do MongoDB e chaves da API de IA:

```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017/PagamentosDb"
  },
  "AIService": {
    "ApiKey": "SUA_CHAVE_DE_API_HERE"
  },
  "JwtSettings": {
    "SecretKey": "SUA_SECRET_KEY_HERE",
    "Issuer": "SUA_ISSUER_NAME_HERE"
  }
}