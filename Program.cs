



TarefaLonga tarefaLonga = new();

tarefaLonga.LoopGrande();






public class TarefaLonga
{
    long teste = 0;

    public void LoopGrande()
    {
        for (int i = 0; i < 9999999999999999999999F; i++)
        {
            for (int j = 0; j < 99999999999999m; j++)            
                teste += j;            
        }
    }
}