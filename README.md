## Executando o  projeto

Para executar o projeto vá em até a raiz do projeto, abra o Power Shell e digite os comandos abaixo:
<br>
> Para buildar o projeto, baixar depedencias, etc...:
```console
 docker-compose build --no-cache
```

> Para subir os containes no docker:
```console
docker-compose up --build
```
<br>

## Acessando o projeto

Após rodar os comandos acima, espere até que todos os containers estejam rodando e então vá até a barra de endereço e digite a url abaixo:
```console
http://localhost:4200/login
```
> Obs: Na primeira vez que o projeto subir vai demorar um pouco para fazer o login, pois o projeto estará criando o banco de dados e aplicando as migrations
<br>

## Sobre o projeto

### Usuários para acessar a aplicação
| Usuário    |  Email          | Senha | Perfil |
|-----------|:---------------:|:-----:|:------:|
| Usuário 1  | user1@gmail.com |  1234 |  User (usuário comum) |     
| Administrador 1  | admin1@gmail.com |  1234 | Admin |        
| Administrador 2  | admin2@gmail.com |  1234 | Admin |      

### Permissões dos usuários
| Permissão    |  Admin    | User | 
|-----------|:---------------:|:-----:|
| Cadastrar nova reserva  | ✅  |  ❌ |  
| Listar reservas  | ✅ (Somente as próprias reservas) |  ✅ (A reserva de todos os usuários) | 
| Ver detalhes da reserva  | ✅ |  ✅ | 
| Editar reservas  | ✅ |  ❌ |
| Deletar reserva  | ✅ |  ❌ | 

<br>

## Algumas regras e observações do sistema


- **Horários pré agendados**<br/>
  O sistema trabalha com horários pré cadastrados no banco de dados (tabela **sala_horario** do banco de dados), eu deixei cadastrado horários do dia 12/08/2024 ao dia 16/08/2024 (não estou validando marcação em datas passadas)

- **Agendamento em horários seguidos**<br/>
  O sistema não permite agendar horários que não são seguidos em uma mesma solicitação, se quiser cadastrar horários não seguidos é necessários realizar 2 solicitações separadas
