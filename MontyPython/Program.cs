using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
namespace MontyPython
{
    public enum DoorState
    {
        OPENED,
        CLOSED,
    }

    public enum Storage
    {
        Zonk,
        Car
    }
    public class Door
    {
        DoorState state;
        Storage storage;
        public Vector2 position;

        int door_number;

        static Texture2D door;
        static Texture2D door_car;
        static Texture2D door_zonk;

        public static void Init()
        {
            door = LoadTexture("assets/door.png");
            door_car = LoadTexture("assets/door_car.png");
            door_zonk = LoadTexture("assets/door_zonk.png");
        }

        public Door(int door_number , DoorState state, Storage storage)
        {
            this.door_number = door_number;
            this.state = state;
            this.storage = storage;
        }

        public void DrawDoorNumber(int door_number)
        {
            DrawTexture(door, (int)position.X, (int)position.Y, Color.WHITE);
            DrawText($"{door_number}", (int)position.X + door.Width/2, (int)position.Y + 20, 15, Color.BLACK);
        }

        public void Draw()
        {
            if (state == DoorState.OPENED)
            {
                if (storage == Storage.Car)
                {
                    DrawTexture(door_car, (int)position.X, (int)position.Y, Color.WHITE);
                }
                else if (storage == Storage.Zonk)
                {
                    DrawTexture(door_zonk, (int)position.X, (int)position.Y, Color.WHITE);
                }
            }
            else if (state == DoorState.CLOSED)
            {
                DrawDoorNumber(door_number);
            }
        }
        public void Open()
        {
            this.state = DoorState.OPENED;
        }

        public void Close()
        {
            this.state = DoorState.CLOSED;
        }

        public int GetWidth()
        {
            if (this.state == DoorState.CLOSED)
            {
                return door.Width;
            }
            else if (this.state == DoorState.OPENED)
            {
                return door_car.Width;
            }
            else
            {
                throw new NotImplementedException("Not implemented yet");
            }
        }

        public int GetHeight()
        {
            if (this.state == DoorState.CLOSED)
            {
                return door.Height;
            }
            else if (this.state == DoorState.OPENED)
            {
                return door_car.Height;
            }
            else
            {
                throw new NotImplementedException("Not implemented yet");
            }
        }

        public Rectangle GetRect()
        {
            Rectangle rect = new Rectangle(position.X, position.Y, GetWidth(), GetHeight());
            return rect;
        }

        public Storage GetStorage()
        {
            return storage;
        }
    }


    public enum GameState
    {
        UserChoose, // use choose his door
        HostOpenDoor,
        HostAsk,
        UserConfirms,
        GameEnd, // end of the game
    }

    class MontyGame
    {
        // extend the game to n numbers of doors
        List<Door> doors;
        
        Door? selected; // select door by player


        bool opened_first;
        bool user_choose;
        bool isWin;

        GameState state;
        public MontyGame(int doors_number) {
            this.doors = new List<Door>(doors_number);
            state = GameState.UserChoose;
        }

        public void Init()
        {
            // Create a list with one Car and (numberOfDoors - 1) Zonks
            List<Storage> outcomes = new List<Storage>();
            outcomes.Add(Storage.Car);

            for (int i = 0; i < doors.Capacity -1; i++)
            {
                outcomes.Add(Storage.Zonk);
            }

            // Randomize the list
            RandomizeList(outcomes);
            int maxDoorsInLine = 10; // Adjust this value based on your needs
            Door fun = new(9999,DoorState.CLOSED, Storage.Zonk); // funny fun
            int door_width = fun.GetWidth();
            int door_height = fun.GetHeight();
            int totalWidth = maxDoorsInLine * door_width + (maxDoorsInLine - 1) * space_between_doors;
            int startX = (GetScreenWidth() - totalWidth) / 2;
            int startY = 100; // Adjust this value based on your needs

            for (int i = 1; i <= doors.Capacity; i++)
            {
                int row = (i - 1) / maxDoorsInLine;
                int col = (i - 1) % maxDoorsInLine;

                doors.Add(new Door(i, DoorState.CLOSED, outcomes[i - 1]));
                doors[i - 1].position = new Vector2(startX + col * (door_width + space_between_doors),
                                                   startY + row * (door_height + vertical_space_between_doors));
            }
        }

        public void Update()
        {
            if (state == GameState.UserChoose)
            {
                for (int i = 1;
                i <= doors.Capacity; i++)
                {
                    if (CheckCollisionPointRec(GetMousePosition(), doors[i - 1].GetRect()))
                    {
                        if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                        {
                            selected = doors[i - 1];
                            state = GameState.HostOpenDoor;
                        }
                    }
                }
            }
            else if (state == GameState.HostOpenDoor)
            {
                List<Door> doorWithZonks = doors.Where(door => door.GetStorage() == Storage.Zonk).ToList();
                for (int i = 0; i < doorWithZonks.Count; i++)
                {
                    if (i != doors.IndexOf(selected))
                    {
                        doorWithZonks[i].Open();
                    }
                }
                state = GameState.UserConfirms;
            }

            
        }

        public void Render()
        {
            DrawText("The Monty Hal Game : try your luck and earn a car", 0, 30, 30, Color.RED);
            for (int i=1; i <= doors.Capacity; i++)
            {
                doors[i-1].Draw();
            }
        }

        // Helper method to shuffle the list randomly
        private void RandomizeList<T>(List<T> list)
        {
            Random rand = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private const int space_between_doors = 20;  // Adjust this value based on the desired space between doors
        private const int vertical_space_between_doors = 20;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            InitWindow(800, 600, "Monty Hal");
            SetTargetFPS(60);
            Door.Init();
            MontyGame game = new(30);
            game.Init();
            while (!WindowShouldClose())
            {
                game.Update();
                BeginDrawing();
                ClearBackground(Color.WHITE);
                game.Render();
                EndDrawing();
            }

            CloseWindow();
        }
    }
}