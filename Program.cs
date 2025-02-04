using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace TextRpg
{
    internal class Program
    {
         
        public class Character // 캐릭터 클래스
        {
            public int lv = 01; // 캐릭터의 레벨 초기값
            public string name = "Chad"; // 캐릭터의 이름 초기값
            public string job = "전사"; // 캐릭터의 직업 초기값
            public float attackPower = 10.0f; // 캐릭터의 공격력 초기값
            public float defense = 5.0f; // 캐릭터의 방어력 초기값
            public float health = 100.0f; // 캐릭터의 체력 초기값
            public float gold = 3000.0f; // 캐릭터의 골드 초기값
            public float itemAttackPower = 0; // 캐릭터의 아이템을 통한 공격력 증가량 초기값
            public float itemDefense = 0; // 캐릭터의 아이템을 통한 방어력 증가량 초기값

            public Item[] Inventory = new Item[] { }; // 생성된 캐릭터의 인벤토리 초기값

            public void AddInventory(Item newItem)  // 아이템을 구매하면 Inventory의 크기가 커지고 아이템을 가져오는 메서드
            {
                Item[] newInventory = new Item[Inventory.Length + 1]; // 기존의 인벤토리보다 크기가 1 큰 배열 생성

                for (int i = 0; i < Inventory.Length; i++)
                {
                    newInventory[i] = Inventory[i]; // 기존 인벤토리를 가져옴
                }

                newInventory[newInventory.Length - 1] = newItem; // 새로운 인벤토리에 새로운 아이템 등록
                Inventory = newInventory; // 기존의 인벤토리에 새로운 인벤토리를 가져옴
            }

            public void EquipItem(Item item) // 아이템 장착에 따른 능력치 변화 메서드
            {
                if(item.Type == 0) // 무기
                {
                    if (item.IsUse) // 장착 했으면
                    {
                        itemAttackPower += item.Ability;
                        attackPower += item.Ability;
                    }
                    else // 장착을 안했으면
                    {
                        itemAttackPower -= item.Ability;
                        attackPower -= item.Ability;
                    }
                }
                else // 방어구
                {
                    if (item.IsUse)
                    {
                        itemDefense += item.Ability;
                        defense += item.Ability;
                    }
                    else
                    {
                        itemDefense -= item.Ability;
                        defense -= item.Ability;
                    }
                }
            }
        }

        public class Item // 아이템 클래스
        {
            public string Name { get; set; } // 아이템 이름
            public float Ability { get; set; } // 아이템 능력치
            public string Info { get; set; } // 아이템 설명
            public float Gold {  get; set; } // 아이템 구입 비용
            public int Type { get; set; } // 아이템의 타입(0: 무기, 1: 방어구)
            public bool IsAdd { get; set; } // 아이템을 구매 했는지
            public bool IsUse {  get; set; } // 아이템을 장착 했는지
            
            public Item(string name, float ability, string info, float gold, int type, bool isAdd, bool isUse) // 아이템 생성자
            {
                Name = name;
                Ability = ability;
                Info = info;
                Gold = gold;
                Type = type;
                IsAdd = isAdd;
                IsUse = isUse;
            }

            public void ItemInfo(int? i) // 인벤토리 창에서 아이템 정보를 가져오는 메서드.
            {
                if(!IsUse) // 장착을 안했으면
                {
                    string abilityType = Type == 0 ? "공격력" : "방어력"; // 삼항 연산자를 통해 무기인지 방어구인지 확인
                    Console.WriteLine($"- {i} {Name} | {abilityType} +{Ability} | {Info}");
                }
                else
                {
                    string abilityType = Type == 0 ? "공격력" : "방어력";
                    Console.WriteLine($"- {i} [E]{Name} | {abilityType} +{Ability} | {Info}");
                }
            }

            public void ShopInfo(int? i) // 상점 구매창에서 아이템 정보를 가져오는 메서드
            {
                if(!IsAdd) // 구매를 안했으면
                {
                    string abilityType = Type == 0 ? "공격력" : "방어력"; // 삼항 연산자를 통해 무기인지 방어구인지 확인
                    Console.WriteLine($"- {i} {Name} | {abilityType} +{Ability} | {Info} | {Gold} G");
                }
                else
                {
                    string abilityType = Type == 0 ? "공격력" : "방어력";
                    Console.WriteLine($"- {i} {Name} | {abilityType} +{Ability} | {Info} | 구매완료");
                }
            }
        }
        static void Main()
        {

            Character c = new Character(); // 캐릭터 생성

            Item[] shopItems = new Item[] // 아이템 생성
            {
                new Item("무쇠갑옷", 5.0f, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000.0f, 1, false, false),
                new Item("스파르타의 창", 7.0f, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000.0f, 0, false, false),
                new Item("낡은검", 2.0f, "쉽게 볼 수 있는 낡은 검 입니다.", 600.0f, 0, false, false)
            };

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine(" ");

            while (true)
            {
                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("");
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">> ");

                int mainInput = int.Parse(Console.ReadLine());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");

                switch(mainInput)
                {
                    case 1:
                        while(true)
                        {
                            Console.WriteLine("상태 보기");
                            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                            Console.WriteLine(" ");
                            Console.WriteLine($"LV. {c.lv:D2}");
                            Console.WriteLine($"{c.name} ( {c.job} )");
                            Console.WriteLine($"공격력 : {c.attackPower} {(c.itemAttackPower > 0 ? $" (+{c.itemAttackPower})" : "")}"); // 3항 연산자를 통해 아이템을 장착하면 장착된 아이템의 능력치의 합을 출력
                            Console.WriteLine($"방어력 : {c.defense} {(c.itemDefense > 0 ? $" (+{c.itemDefense})" : " ")}");            // 3항 연산자를 통해 아이템을 장착하면 장착된 아이템의 능력치의 합을 출력
                            Console.WriteLine($"체 력 : {c.health}");
                            Console.WriteLine($"Gold : {c.gold} G");
                            Console.WriteLine(" ");
                            Console.WriteLine("0. 나가기 ");
                            Console.WriteLine(" ");
                            Console.WriteLine("원하시는 행동을 입력해주세요.");
                            Console.Write(">> ");

                            int stateInput = int.Parse(Console.ReadLine());
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------");

                            if (stateInput != 0)
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                            }
                            else // 상태창 나가기
                            {
                                break;
                            }
                        }
                        break;
                    case 2:
                        while(true)
                        {
                            Console.WriteLine("인벤토리");
                            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                            Console.WriteLine("");
                            Console.WriteLine("[아이템 목록]");
                            Console.WriteLine("");

                            bool hasItems = false; // 아이템 존재 여부

                            for (int i = 0; i < c.Inventory.Length; i++) // 인벤토리의 아이템 정보를 가져오는 반복문
                            {
                                c.Inventory[i].ItemInfo(null); // 인벤토리의 아이템 정보를 가져옴 null 값을 통해 구매 목록 번호 표시 X
                                hasItems = true;
                            }

                            if (hasItems) // 아이템이 있으면 출력
                            {
                                Console.WriteLine(" ");
                                Console.WriteLine("1. 장착 관리 ");
                                Console.WriteLine("2. 나가기 ");
                                Console.WriteLine(" ");
                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                Console.Write(">> ");
                            }
                            else // 아이템이 없으면 출력
                            {
                                Console.WriteLine(" ");
                                Console.WriteLine("1. 장착 관리 ");
                                Console.WriteLine("0. 나가기 ");
                                Console.WriteLine(" ");
                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                Console.Write(">> ");
                            }

                            int equipInput = int.Parse(Console.ReadLine());
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------");

                            if (equipInput == 1 && hasItems) // 장착관리를 선택 하면서, 아이템이 있는 경우
                            {
                                while (true)
                                {
                                    Console.WriteLine("인벤토리 - 장착 관리");
                                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                                    Console.WriteLine("");
                                    Console.WriteLine("[아이템 목록]");
                                    Console.WriteLine("");

                                    for (int i = 0; i < c.Inventory.Length; i++) 
                                    {
                                        c.Inventory[i].ItemInfo(i + 1); // 아이템 정보를 가져오고 i+1을 통해 구매 목록 번호를 표시
                                    }

                                    Console.WriteLine(" ");
                                    Console.WriteLine("0. 나가기 ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                                    Console.Write(">> ");

                                    int wearInput = int.Parse(Console.ReadLine());
                                    Console.WriteLine("-------------------------------------------------------------------------------------------------------");

                                    // 유효한 아이템 목록 번호를 눌렀고 착용중이 아니라면
                                    if (wearInput > 0 && wearInput <= c.Inventory.Length && !c.Inventory[wearInput - 1].IsUse)
                                    {
                                        c.Inventory[wearInput - 1].IsUse = true; // 아이템 착용 
                                        c.EquipItem(c.Inventory[wearInput - 1]); // 아이템 착용으로 인한 능력치 변화 메서드 호출
                                    }
                                    else if (wearInput > 0 && wearInput <= c.Inventory.Length && c.Inventory[wearInput - 1].IsUse)
                                    {
                                        c.Inventory[wearInput - 1].IsUse = false; // 아이템 해제
                                        c.EquipItem(c.Inventory[wearInput - 1]); // 아이템 해제로 인한 능력치 변화 메서드 호출
                                    }
                                    else if (wearInput == 0) // 장착 관리 창 나가기
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("잘못된 입력입니다. ");
                                        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                                    }
                                }
                            }
                            else if (equipInput == 1 && !hasItems) // 장착관리를 선택 하면서, 아이템이 없는 경우
                            {
                                Console.WriteLine("아이템이 없습니다.");
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                            }
                            else if (equipInput == 0 && !hasItems) // 나가기, 아이템이 없는 경우
                            {
                                break;
                            }
                            else if (equipInput == 2 && hasItems) // 나가기, 아이템이 있는 경우
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                            }
                        }
                        break;
                    case 3:
                        while(true)
                        {
                            Console.WriteLine("상점");
                            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                            Console.WriteLine("");
                            Console.WriteLine("[보유골드]");
                            Console.WriteLine($"{c.gold} G");
                            Console.WriteLine("");
                            Console.WriteLine("[아이템 목록]");

                            for (int i = 0; i < shopItems.Length; i++) // 생성된 아이템 정보 가져오는 반복문
                            {
                                shopItems[i].ShopInfo(null); // 아이템 정보 상점 표시 메서드 호출 null 값을 통해 상점의 아이템 목록 번호 표시 X
                            }

                            Console.WriteLine("");
                            Console.WriteLine("1. 아이템 구매");
                            Console.WriteLine("0. 나가기");
                            Console.WriteLine("");
                            Console.WriteLine("원하시는 행동을 입력해주세요.");
                            Console.Write(">> ");

                            int shopInput = int.Parse(Console.ReadLine());
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------");

                            if(shopInput == 1) // 아이템을 구매를 선택
                            {
                                while(true)
                                {
                                    Console.WriteLine("상점 - 아이템 구매");
                                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                                    Console.WriteLine("");
                                    Console.WriteLine("[보유골드]");
                                    Console.WriteLine($"{c.gold} G");
                                    Console.WriteLine("");
                                    Console.WriteLine("[아이템 목록]");

                                    for (int i = 0; i < shopItems.Length; i++)
                                    {
                                        shopItems[i].ShopInfo(i + 1); //아이템 정보 상점 표시 메서드 호출 i + 1  값을 통해 상점의 아이템 목록 번호 표시
                                    }

                                    Console.WriteLine("");
                                    Console.WriteLine("0. 나가기");
                                    Console.WriteLine("");
                                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                                    Console.Write(">> ");

                                    int selectedInput = int.Parse(Console.ReadLine());
                                    Console.WriteLine("-------------------------------------------------------------------------------------------------------");

                                    if (selectedInput > 0 && shopInput <= shopItems.Length) // 유효한 아이템 목록 번호를 입력 했다면
                                    {
                                        Item selectedItem = shopItems[selectedInput - 1]; // 해당 아이템을 가져온다

                                        if(!selectedItem.IsAdd) // 아이템을 구매하지 않았다면
                                        {
                                            if (c.gold >= selectedItem.Gold) // 보유 골드가 아이템 가격보다 많거나 같다면
                                            {
                                                c.AddInventory(selectedItem); // 인벤토리 확장 및 인벤토리의 해당 아이템 가져오기 메서드 호출
                                                c.gold -= selectedItem.Gold; // 골드 감소
                                                selectedItem.IsAdd = true; // 아이템 구매 확인 bool값
                                                Console.WriteLine("구매를 완료했습니다.");
                                                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Gold가 부족합니다.");
                                                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("이미 구매한 아이템 입니다.");
                                            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                                        }
                                    }
                                    else if(selectedInput == 0) // 상점 구매창 나가기
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("잘못된 입력입니다.");
                                        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                                    }
                                }
                            }
                            else if (shopInput == 0) // 상점창 나가기
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                        break;
                }
            }    
            
        }
    }
}
