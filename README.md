# Cliente e Servidor de Chat TCP
Este projeto utiliza diretamente do protocolo TCP/IP para fazer um comunicao de CHAT utilizando um protocolo própio chamado de CCM (Chat Communication Message).

## CCM
Chat Communication Message é um protocolo baseado em HTTP para fazer a comunicação entre Cliente e servidor de um aplicativo de Chat.

### Esquema da mensagem 
Uma mensagem CCM é dividida em 3 partes principais: 
- **Cabeçalho:** Informações da mensagem como remetente, destinatário, tipo de mensagem e etc..
 - _Versão:_  Versão do Protocolo CCM
 - _Tipo:_ Tipo de mensagem (Audio, Conexão, Streaming e etc..)
 - _Atributos:_ Atributos personalizados.
- **Separador:** Um carácter ou conjunto de carácters usandos para separar o _Cabeçalho_ do _Contéudo_.
- **Contéudo:** Contéudo da mensagem que será enviado para o destinatário, este contéudo deve estar códificado para bytes e deve ser especificado seu tipo no no campo "Tipo" descrito no cabeçalho, além do seu tipo deve ser definido seu codificador (ASCII, UTF-8, Unicode e etc..) também no cabeçalho em atributos com o nome de "Message-Encoding".


	**----Inicio do cabeçalho----**
	CCM\{Versão} {Tipo}
	{Chave}: {Atributo}
	{Separador}
	**----Fim do cabeçalho----**
	{Contéudo}
