namespace MonsterPang_Console
{
    class Monster
    {
        public enum MonsterType { nine = 1, girl, dracula, zombie, tobi, end }
        public MonsterType type;
        public double hp;

        public Monster(int level)
        {
            hp = level * 50;

            switch (level)
            {
                case (int)MonsterType.nine:
                    type = MonsterType.nine;
                    break;
                case (int)MonsterType.girl:
                    type = MonsterType.girl;
                    break;
                case (int)MonsterType.dracula:
                    type = MonsterType.dracula;
                    break;
                case (int)MonsterType.zombie:
                    type = MonsterType.zombie;
                    break;
                case (int)MonsterType.tobi:
                    type = MonsterType.tobi;
                    break;
                default:
                    type = MonsterType.end;
                    break;
            }
        }
    }
}
