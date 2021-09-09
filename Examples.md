# Exemplo de Mensagens CCM
Este arquivo contém alguns exemplos de mensagens usando o protocolo CCM, exemplos contidos aqui:

- :x:  Inicio Conexão
- :x: Fim conexão 
- :heavy_check_mark: Envio de texto
- :x: Recebimento texto
- :x: Envio de Arquivo 
- :x: Recebimento Arquivo
- :x: Envio Streaming 
- :x: Recebimento Streaming

> O símbolo ":heavy_check_mark:" indica que este exemplo foi implementado, já o simbolo ":x:" que está a ser implementado.

## Exemplo envio de texto
Este exemplo mostra como enviar o texto _"Este é um exemplo de mensagem enviado ao usuário 'User123'. "_ ao usuário do "User123" utilizando a codificação _UTF-8_, com o token de usuário "AAAAAAAAAAAAAA".
	
    CCM\1.0 Text
    User: AAAAAAAAAAAAAA
    Addressee: User123
    Message-Encoding: UTF-8
    
    Este é um exemplo de mensagem enviado ao usuário 'User123'.
