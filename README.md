# DesafioVivo
Alguns detalhes que eu poderia sugerir como melhorias, mas não foram aplicados, pois o projeto poderia crescer além de um teste.

- A modelagem de dados poderia ser melhorada, como por exemplo:
	Separar uma tabela para documentos (para permitir outros além de RG e CPF, sem necessidade de criar novos campos na tabela);
	Uma pra endereço (visando diminuir a quantidade de registros na tabela, já que pode haver mais de um morador no mesmo endereço);
	E a criação de um status de usuário que permitira outros status além de ativo ou não.

- Não foi possível rodar completamente o banco de dados no docker em minha máquina. Para não atrasar o teste, deixei comentado as alterações que seriam feitas para usar na imagem no Docker, então o projeto só roda executando pelo Visual Studio neste momento. 


