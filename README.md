# Cliente e Servidor de Chat TCP
Este projeto foi feito por [Juan Douglas](https://github.com/JuanDouglas) a ideia é criar um cliente e servidor para simular um chat. O projeto utiliza diretamente o protocolo _TCP/IP_ para fazer um comunicação, além disso utiliza um protocolo própio chamado de _CCM_ (Chat Communication Message) para enviar e receber mensagens ou streaming.
> Este Readme contém detalhes sobre o ideia, para saber mais sobre o projeto e seu esquema click [nesse link](Resources/Help/Project.md).

## CCM
Chat Communication Message é um protocolo baseado em HTTP para criado para fazer a comunicação entre Cliente e Servidor este protocolo não segue nenhum padrão da industria este protocolo e de carácter apenas de estudo de _TCP/IP_, sendo assim não tem como foco segurança e nem praticidade, para solucionar este "problema" poderia ser usado perfeitamente o HTTP, que junto com HTTPS daria praticidade, segurança e agilidade.

> Caso alguma mensagem não esteja seguindo o protocolo _CCM_ o servidor irá responder usando HTTP com o seguinte contéudo "This server uses the CCM (Chat Comunitcation message) protocol to communicate and I need to use this protocol!".

> Para ver exemplos de mensagens usando o protocolo _CCM_ veja esse [link](Resources/Help/Examples.md).


### Esquema de mensagens
Uma mensagem do protcolo CCM é dividida em 3 partes principais: 
- **Cabeçalho:** Informações da mensagem como remetente, destinatário, tipo de mensagem e etc..
	- _Versão:_  Versão do Protocolo CCM
	- _Tipo:_ Tipo de mensagem (Audio, Conexão, Streaming e etc..)
	- _Atributos:_ Atributos personalizados.
	> **Nota:** Todo o cabeçalho deve ser codificado na codificação configurada no arquivo "Configuration.json", dentro do projeto Chat.Server na pasta "Server".
- **Separador:** Um carácter ou conjunto de carácters usandos para separar o _Cabeçalho_ do _Contéudo_.
- **Contéudo:** Contéudo da mensagem que será enviado para o destinatário
> **Nota:** Este contéudo deve estar códificado para bytes e deve ser especificado seu tipo no no campo "Tipo" descrito no cabeçalho, além do seu tipo deve ser definido seu codificador (ASCII, UTF-8, Unicode e etc..) também no cabeçalho em atributos com o nome de "Message-Encoding".

	**----Inicio do cabeçalho----**
	CCM\{Versão} {Tipo}
	{Chave}: {Atributo}
	{Separador}
	**----Fim do cabeçalho----**
	{Contéudo}
    
### Fluxo de mensagens 
A comunicação com o servidor tem 3 grandes fases.
- **Idêntificação e conexão:** Nesta fase se idêntifica o usuário usando o [OAuth](https://github.com/JuanDouglas/OAuth), durante este processo o usuário vai obter o token único que servirá para idêntificalo neste servidor.
- **Envio e recebimento de mensagens:** Nesta fase o usuário já deve estar idêntificado, estando devidamente idêntificado o servidor irá receber e enviar mensagens de usuários para o usuário idêntificado sem uma ordem definida.
- **Finalização:** Tanto o servidor quanto o cliente podem finalizar a comunicação (o servidor geralmente finaliza por errors no protocolo ou quando não nenhuma resposta), finalizar a comunicação irá desqualificar o token de usuário obtido anteriormente. 

> Após a fase de identificação o servidor irá mandar no período de 5000ms uma mensagem para continuar a comunicação, caso não haja nenhuma resposta em até 3600ms o servidor irá finalizar a comunicação (*Este é o tempo que o servidor deve receber a mensagem). 
