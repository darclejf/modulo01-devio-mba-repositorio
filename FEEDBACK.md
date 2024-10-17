# Feedback do Instrutor

#### 17/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Boa separação de responsabilidades.
- Demonstrou conhecimento em Identity e JWT
- Bom uso de mapeador AutoMapper
- Arquitetura enxuta de acordo com a complexidade do projeto
- Uso correto da abstração do Identity ao controlar o processo para criação de uma entidade autor durante a criação do usuário.
- Mostrou entendimento do ecossistema de desenvolvimento em .NET

## Pontos Negativos:

- Aparentemente o projeto está incompleto na parte das aplicações Web, as controllers não fazem controle de usuário ou não possuem comportamentos implementados.

## Sugestões:

- A camada Data poderia se chamar "Core" ou "Application", pois confere mais poder a tudo que esta camada está fazendo.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
