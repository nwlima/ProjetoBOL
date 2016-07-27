# ProjetoBOL
Webservices for SOA Classes

A new project regarding SOA Classes and Training.


A BOL (Bike on Line) é uma montadora e vendedora de bicicletas que comercializa exclusivamente pela internet. Só há um modelo (BolMax). Há 3 fornecedores : para quadros (Kit Q), pneus(Kit P) e outras peças (Kit O). Quando o usuário faz uma cotação, o sistema verifica se há bicicletas prontas em estoque. Se houver, calcula o frete (em função do CEP) e o soma ao preço da bicicleta. O prazo informado é o da transportadora. Se o usuário confirmar o pedido (com o código da cotação), é solicitado o cpf e o cartão de crédito. Se a compra for autorizada no cartão, é disparada a ordem de expedição. Se não houver bicicleta pronta em estoque, é verificado se há estoque de kits Q, P e O. Se houver os 3 kits, o processo repete-se, somando-se ao prazo de frete o prazo de 1 dia (montagem). Se houver falta de um de kits, é comunicada indisponibilidade do produto.
Diariamente é feita a verificação do estoque mínimo de cada um dos kits. Se um deles estiver abaixo do estoque mínimo, é feita cotação para 3 fornecedores homologados. Ao final desse processo, são gerados pedidos aos fornecedores que apresentarem menor preço (máximo é configurado) dentro do prazo máximo e 10 dias. Se nenhum fornecedor apresentar as condições esperadas é gerado alerta.
Projeto :
1)Atividades :
a)Mapeamento dos Processos (bizagi)
b)Análise Orientada a Serviços (definição de serviços e camadas)
c)Implementação dos serviços (todas as camadas, exceto orquestração especializada)
d)Implementação do BPEL
2. Equipes : Até 5 pessoas
3.Prazos :
Entrega Parcial (aula 10) : atividades a e b (40% da nota de projeto)
Entrega Final : (aula 13) (60% da nota de projeto)
4. Entregas :
Processos mapeados, SOA, códigos de ws e bpel.
5. Apresentação : Rodar o Bpel direto do Oracle (se possível, ou gerar interface asp.net para chama-lo)

