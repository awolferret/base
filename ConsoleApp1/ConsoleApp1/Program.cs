using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            DataBase dataBase = new DataBase();
            dataBase.Working();
        }
    }

    class DataBase 
    {
        private bool _isWorking = true;
        private List<Player> _players = new List<Player>();
        int emptyDataBaseCode = 1;
        int wrongInputCode = 2;
        public void Working()
        {
            while (_isWorking)
            {
                Console.WriteLine("1. Добавить игрока");
                Console.WriteLine("2. Показать список игроков");
                Console.WriteLine("3. Забанить игрока");
                Console.WriteLine("4. Разбанить игрока");
                Console.WriteLine("5. Удалить игрока");
                Console.WriteLine("6. Выход");
                Console.WriteLine("Введите команду");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddPlayer();
                        break;
                    case "2":
                        ShowPLayersList();
                        break;
                    case "3":
                        BannPlayer();
                        break;
                    case "4":
                        UnBannPlayer();
                        break;
                    case "5":
                        RemovePlayer();
                        break;
                    case "6":
                        Exit();
                        break;
                    default :
                        ShowWarningMessage(wrongInputCode);
                        break;
                }
            }
        }

        public void AddPlayer()
        {
            int wrongInputCode = 2;
            Console.WriteLine("Ведите никнейм нового пользователя");
            string nickname = Console.ReadLine();
            Console.WriteLine("Ведите уровень нового пользователя");
            string input = Console.ReadLine();
            int playerLevel;

            if (ChekInput(input, out playerLevel))
            {
                _players.Add(new Player(nickname, playerLevel));
            }
            else 
            {
                ShowWarningMessage(wrongInputCode);
            }

            Console.Clear();
        }

        public void BannPlayer()
        {
            if (_players.Count > 0)
            {
                Console.WriteLine("Введите id пользователя вы хотите забанить?");
                string input = Console.ReadLine();
                int number = 0;

                if (TryGetPLayer(input, number))
                {
                    _players[number - 1].Ban();
                    Console.Clear();
                }
                else
                {
                    ShowWarningMessage(wrongInputCode);
                }
            }
            else
            {
                ShowWarningMessage(emptyDataBaseCode);
            }
        }

        public void UnBannPlayer()
        {
            if (_players.Count > 0)
            {
                Console.WriteLine("Введите id пользователя вы хотите разбанить?");
                string input = Console.ReadLine();
                int number = 0;

                if (TryGetPLayer(input, number))
                {
                    _players[number - 1].UnBan();
                    Console.Clear();
                }
                else
                {
                    ShowWarningMessage(wrongInputCode);
                }
            }
            else
            {
                ShowWarningMessage(emptyDataBaseCode);
            }
        }

        public void RemovePlayer()
        {
            if (_players.Count > 0)
            {
                Console.WriteLine("Введите id пользователя вы хотите удалить?");
                string input = Console.ReadLine();
                int number;

                if (ChekInput(input, out number) && number <= _players.Count)
                {
                    _players.RemoveAt(number - 1);
                    Console.Clear();
                }
                else
                {
                    ShowWarningMessage(wrongInputCode);
                }
            }
            else
            {
                ShowWarningMessage(emptyDataBaseCode);
            }
        }

        public void ShowPLayersList()
        {
            if (_players.Count > 0)
            {
                Console.WriteLine("Список:");

                for (int i = 0; i < _players.Count; i++)
                {
                    Console.Write($"{i + 1}.");
                    _players[i].ShowData();
                }

                Console.WriteLine("Что бы продожить нажмите любую клавишу");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                ShowWarningMessage(emptyDataBaseCode);
            }
        }

        private string ShowWarningMessage(int warningCode)
        {
            string message = " ";
            switch (warningCode)
            {
                case 1:
                    Console.WriteLine("В базе данных пусто, что бы продожить нажмите любую клавишу");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Console.WriteLine("Некоректный ввод, что бы продожить нажмите любую клавишу");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
            return message;
        }

        private bool ChekInput(string input, out int number)
        {
            bool isCorrect = false;
            isCorrect = int.TryParse(input, out number);
            return isCorrect;
        }

        public bool Exit()
        {
            _isWorking = false;
            return _isWorking;
        }

        public bool TryGetPLayer(string input, int number)
        {
            if (ChekInput(input, out number) && number <= _players.Count && (_players[number - 1].IsBanned == true))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }

    class Player
    {
        private string _nickname;
        private int _playerLevel;
        public bool IsBanned { get; private set; }

    public Player(string nickname, int playerLevel)
        {
            IsBanned = false;
            _playerLevel = playerLevel;
            _nickname = nickname;
        }

        public string ShowBanStatus()
        {
            string banstatus = " ";

            if (IsBanned == true)
            {
                banstatus = "Забанен";
                return banstatus;
            }
            else
            {
                banstatus = "Не забанен";
                return banstatus;
            }
        }

        public void UnBan()
        {
            IsBanned = false;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void ShowData()
        {
            Console.WriteLine($"Ник {_nickname}, уровень {_playerLevel}, статус бана {ShowBanStatus()}");
        }
    }
}