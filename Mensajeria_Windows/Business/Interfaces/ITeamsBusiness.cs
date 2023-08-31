namespace Mensajeria_Windows.Business.Interfaces
{
    public interface ITeamsBusiness
    {
         Task<int> EnviarTeamsSimple (string webHookUrl, string titulo, string texto);
    }
}
