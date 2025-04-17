namespace TextRPG_assignment2
{
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;


  
   
        class Item
        {
        public string Name;
        public string Description;
        public int Price;
        public int Power;

        public Item(string name, string description, int price, int power)
        {
            Name = name;
            Description = description;
            Price = price;
            Power = power;
        }
    }

    internal class Program
    {
        static string playerName = "";
        static string playerJob = "";
        static int attack = 0;
        static int defense = 0;
        static int hp = 0;
        static int gold = 1500;

        static List<string> inventory = new List<string>();
        static List<string> equippedItems = new List<string>(); //  여러 개 장착 가능
        static List<string> purchasedItems = new List<string>();

        static List<Item> shopItems = new List<Item>
        {
            new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 300, 5),
            new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 500, 9),
            new Item("스파르타의 갑옷", "스파르타 전사들이 사용했던 전설의 갑옷입니다.", 900, 15),
            new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 400, 2),
            new Item("청동 도끼", "어디선가 사용됐던 것 같은 도끼입니다.", 600, 5),
            new Item("스파르타의 창", "스파르타 전사들이 사용했던 전설의 창입니다.", 1000, 7)
        };

        static void Main(string[] args)
        {
            Console.WriteLine("스파르타 마을에 오신 것을 환영합니다!");
            Console.Write("당신의 이름을 입력해주세요: ");
            Console.WriteLine();
            playerName = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("직업을 선택해주세요:");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적");

            bool validJob = false;
            while (!validJob)
            {
                Console.Write("직업 번호를 입력하세요 (1~2): ");
                string jobInput = Console.ReadLine();
                Console.WriteLine();

                switch (jobInput)
                {
                    case "1":
                        playerJob = "전사";
                        attack = 10;
                        defense = 5;
                        hp = 100;
                        validJob = true;
                        break;
                    case "2":
                        playerJob = "도적";
                        attack = 12;
                        defense = 3;
                        hp = 70;
                        validJob = true;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 1~2 중에서 선택해주세요.\n");
                        break;
                }
            }

            Console.WriteLine($"환영합니다, {playerName}님! 당신은 {playerJob}입니다.\n");

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("===== 스파르타 마을 =====");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("0. 종료");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        ShowStatus();
                        break;
                    case "2":
                        ShowInventory();
                        break;
                    case "3":
                        ShowShop();
                        break;
                    case "0":
                        Console.WriteLine("게임을 종료합니다.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 0~3 중 하나를 입력해주세요.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static Item FindItemByName(string name)
        {
            return shopItems.Find(item => item.Name == name);
        }

        static void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[상태 보기]");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                int bounsAttack = 0;
                int bounsDefense = 0;

                foreach (string itemName in equippedItems)
                { 
                    Item item = FindItemByName(itemName);
                    if (item != null)
                    {
                        if (item.Name.Contains("갑옷"))
                        {
                            bounsDefense += item.Power;
                        }
                        else
                        { 
                            bounsAttack += item.Power;
                        }
                    }
                }

                int totalAttack = attack + bounsAttack;
                int totalDefense = defense + bounsDefense;

                Console.WriteLine($"Lv. 01");
                Console.WriteLine($"{playerName} ( {playerJob} )");
                Console.WriteLine($"공격력 : {totalAttack} (+{bounsAttack})");
                Console.WriteLine($"방어력 : {totalDefense} (+{bounsDefense})");
                Console.WriteLine($"체 력 : {hp}");
                Console.WriteLine($"Gold : {gold} G\n");

                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
            }
        }

        static void ShowInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[인벤토리]");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                if (inventory.Count == 0)
                {
                    Console.WriteLine("아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        string item = inventory[i];
                        if (equippedItems.Contains(item))
                            Console.WriteLine($"[{i + 1}] [E] {item} (장착됨)");
                        else
                            Console.WriteLine($"[{i + 1}] {item}");
                    }
                }

                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("2. 장착 해제");
                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "1")
                {
                    if (inventory.Count == 0)
                    {
                        Console.WriteLine("\n장착할 아이템이 없습니다.");
                        Console.WriteLine("아무 키나 누르면 돌아갑니다...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("\n장착할 아이템 번호를 입력하세요: ");
                        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= inventory.Count)
                        {
                            string itemToEquip = inventory[index - 1];
                            if (equippedItems.Contains(itemToEquip))
                            {
                                Console.WriteLine($"[ {itemToEquip} ] 은(는) 이미 장착 중입니다.");
                            }
                            else
                            {
                                equippedItems.Add(itemToEquip);
                                Console.WriteLine($"[ {itemToEquip} ] 을(를) 장착했습니다!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("올바른 번호를 입력해주세요.");
                        }

                        Console.WriteLine("아무 키나 누르면 돌아갑니다...");
                        Console.ReadKey();
                    }
                }
                else if (input == "2") //장착 해제 처리
                {
                    if (equippedItems.Count == 0)
                    {
                        Console.WriteLine("\n현재 장착된 아이템이 없습니다.");
                        Console.WriteLine("아무 키나 누르면 돌아갑니다...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\n[장착 해제 가능한 아이템 목록]");
                        for (int i = 0; i < equippedItems.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {equippedItems[i]}");
                        }

                        Console.Write("\n해제할 아이템 번호를 입력하세요: ");
                        if (int.TryParse(Console.ReadLine(), out int unequipIndex) &&
                            unequipIndex >= 1 && unequipIndex <= equippedItems.Count)
                        {
                            string unequippedItem = equippedItems[unequipIndex - 1];
                            equippedItems.Remove(unequippedItem);
                            Console.WriteLine($"\n[ {unequippedItem} ] 을(를) 장착 해제했습니다!");
                        }
                        else
                        {
                            Console.WriteLine("올바른 번호를 입력해주세요.");
                        }

                        Console.WriteLine("아무 키나 누르면 돌아갑니다...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("아무 키나 누르면 돌아갑니다...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowShop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine($"[보유 골드]\n{gold} G\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < shopItems.Count; i++)
                {
                    Item item = shopItems[i];
                    string statInfo = item.Name.Contains("갑옷") ? $"방어력 +{item.Power}" : $"공격력 +{item.Power}";

                    string status = purchasedItems.Contains(item.Name)
                        ? "구매 완료"
                        : $"{item.Price,5} G";

                    Console.WriteLine($"{i + 1}. {item.Name,-15} | {statInfo,-12} | {item.Description,-40} | {status}");
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.Write("\n>> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    return;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= shopItems.Count)
                {
                    Item selectedItem = shopItems[choice - 1];

                    if (purchasedItems.Contains(selectedItem.Name))
                    {
                        Console.WriteLine($"\n[ {selectedItem.Name} ] 은(는) 이미 구매한 아이템입니다.");
                    }
                    else if (gold >= selectedItem.Price)
                    {
                        gold -= selectedItem.Price;
                        inventory.Add(selectedItem.Name);
                        purchasedItems.Add(selectedItem.Name);
                        Console.WriteLine($"\n[ {selectedItem.Name} ] 구매 완료!");
                    }
                    else
                    {
                        Console.WriteLine($"\n골드가 부족합니다. 현재 골드: {gold} G");
                    }
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 다시 입력해주세요.");
                }

                Console.WriteLine("\n아무 키나 누르면 계속...");
                Console.ReadKey();
            }
        }
    }
}
