namespace Cadastramento.Core
{
    public enum Status
    {
        Ok,
        Aviso,
        Erro
    }

    public enum TipoBotao
    {
        Branco,
        Azul,
        Amarelo,
        Vermelho,
        Verde,
        Azulclaro,
        Violeta
    }

    public enum TamanhoBotao
    {
        Grande,
        Padrao,
        Pequeno,
        ExtraPequeno
    }

    public enum TipoMensagem
    {
        Azul,
        Amarelo,
        Vermelho,
        Verde,
        Azulclaro
    }

    public enum TipoMensagemPadrao
    {
        Inclusao = 1,
        Alteracao = 2,
        Exclusao = 3,
        ErroPadrao = 4,
        OperacaoSucesso = 5
    }
}
