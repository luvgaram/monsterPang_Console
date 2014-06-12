
namespace MonsterPang_Console
{
    class Program
    {
    	static void Main(string[] args)
    	{
        	int level = 1;
        	while (level < 6)
        	{
            	Stage stage = new Stage(level);
            	stage.Play();
            	level++;
        	}
    	}

        /*은주 테스트용
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Refresh();
            board.ShowBoard();
            board.Deletable();
            board.Sort();
            //board.ShowBoard();
        }
        */
    }
}
