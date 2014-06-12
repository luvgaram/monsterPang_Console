namespace MonsterPang_Console
{
    class Monster
    {
        public enum MonsterType { goblin = 1, girl, dracula, zombie, kama, end }
        public MonsterType type;
        public double hp;

        public Monster(int level)
        {
            hp = level * 50;

            switch (level)
            {
                case (int)MonsterType.goblin:
                    type = MonsterType.goblin;
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
                case (int)MonsterType.kama:
                    type = MonsterType.kama;
                    break;
                default:
                    type = MonsterType.end;
                    break;
            }
        }
    }
}
