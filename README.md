# Lista-de-Tarefas-em-Docker

Este é um projeto para gerenciar uma lista de tarefas, configurado para rodar com Docker e Docker Compose.

Pode utilizar o Docker Desktop - "https://www.docker.com/products/docker-desktop/".

Este projeto usa **Linux containers**. Se você estiver utilizando Docker Desktop no Windows ou Mac, certifique-se de que ele está configurado para usar containers do tipo **Linux**.
Uma forma simples de verificar, se estiver usando o Docker Desktop, é clicando com o botão direito em seu icone na barra de tarefas. Se encontrar a opção "Switch to windows containers" é porque você já esta usando Linux. Caso encontre "linux" no lugar do windows, é só clicar.

Para subir o projeto: após fazer o clone, o arquivo "docker-compose.yml" está na raiz.

Usar o comando "docker-compose up --build", isto irá construir a imagem da aplicação a partir do Dockerfile, Subir o banco de dados SQL Server e iniciar todos os serviços definidos no docker-compose.yml.

A API estará disponível em http://localhost:8080/swagger

O SQL Server estará disponível em:
- Servidor: localhost, 1433
- Usuário: sa
- Senha: Your@@_password123
