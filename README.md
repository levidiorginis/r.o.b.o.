# r.o.b.o.

Desenvolvemos uma interface para controlar o projeto R.O.B.O. (Robô Operacional Binariamente Orientado) via API Web Restful. É possível visualizar os estados atuais e enviar comandos. A interface gráfica fica em aberto, sem necessidade de animações ou representações figurativas.

# Tutorial para Iniciar o Projeto

Este documento explica como configurar e iniciar o projeto, incluindo os requisitos necessários e os passos para rodar tanto o front-end quanto o back-end.

## Requisitos

Antes de começar, certifique-se de que os seguintes requisitos estão instalados na sua máquina:

- **SDK do .NET 8**

  - [Download do .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

- **Node.js versão 20.18**
  - Verifique sua versão com o comando:
    ```bash
    node -v
    ```
  - [Download do Node.js](https://nodejs.org/)

## Iniciando o Projeto

### 1. Front-end (Client)

1. Navegue até a pasta do cliente:

   ```bash
   cd client
   ```

2. Instale as dependências (se necessário):

   ```bash
   npm install
   ```

3. Inicie o servidor de desenvolvimento:

   ```bash
   npm run dev
   ```

4. O front-end estará disponível em:
   ```
   http://localhost:5173
   ```
   (ou outro endereço indicado no terminal).

### 2. Back-end (Server)

1. Navegue até a pasta da API:

   ```bash
   cd server/Api
   ```

2. Inicie a aplicação .NET:

   ```bash
   dotnet run
   ```

3. O back-end estará disponível em:
   ```
   http://localhost:5072
   ```
   (ou outro endereço indicado no terminal).

## Verificação

- Certifique-se de que o front-end e o back-end estão rodando corretamente.
- Acesse a URL do front-end no navegador para testar a aplicação.
- Use ferramentas como Postman ou o navegador para verificar as rotas do back-end (ex.: `http://localhost:5072/api/v1/Robot`).

Se encontrar problemas, revise os logs do terminal para identificar e corrigir possíveis erros.

---

Com isso, você estará pronto para começar a trabalhar no projeto! Caso tenha dúvidas, consulte a documentação ou entre em contato com o responsável pelo projeto.
