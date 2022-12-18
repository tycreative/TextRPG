using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public interface AreaDelegate {
        public string Desc { get; }
        public void Enter(Player player);
    }

    // If area contains a monster
    public class MonsterArea : AreaDelegate {
        private Monster monster;

        public string Desc { get { return $"A {monster.Name} engages you."; } }

        public MonsterArea(Monster monster) {
            this.monster = monster;
        }

        // Upon player entering the area, change player states
        public void Enter(Player player) {
            player.State = new FightingState(player, monster);
        }
    }

    // If area contains a merchant
    public class MerchantArea : AreaDelegate {
        private Merchant merchant;

        public string Desc { get { return $"A merchant is present."; } }

        public MerchantArea(Merchant merchant) {
            this.merchant = merchant;
        }

        // Upon player entering the area, change player states
        public void Enter(Player player) {
            player.State = new NormalState(player, merchant);
        }
    }
}
