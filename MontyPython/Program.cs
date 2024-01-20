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
    }


    public enum GameState
    {
        UserChoose, // use choose his door
        HostAsk,
        UserConfirms,
        GameEnd, // end of the game
    }

    class MontyGame
    {
        Door door1;
        Door door2;
        Door door3;

        Door selected; // select door by player


        bool opened_first;
        bool user_choose;
        bool isWin;

        public MontyGame() { }

        public void Init()
        {
            List<Storage> outcomes = new List<Storage> { Storage.Car, Storage.Zonk, Storage.Zonk };
            RandomizeList(outcomes);

            // Assign outcomes to the doors
            door1 = new(1, DoorState.CLOSED, outcomes[0]);
            door2 = new(2, DoorState.CLOSED, outcomes[1]);
            door3 = new(3, DoorState.CLOSED, outcomes[2]);

            int totalWidth = 3 * door_width + 2 * space_between_doors;
            int startX = (GetScreenWidth() - totalWidth) / 2;

            door1.position = new Vector2(startX, GetScreenHeight() / 2);
            door2.position = new Vector2(startX + door_width + space_between_doors, GetScreenHeight() / 2);
            door3.position = new Vector2(startX + 2 * (door_width + space_between_doors), GetScreenHeight() / 2);
        }

        public void Update()
        {
            if (CheckCollisionPointRec(GetMousePosition(), door1.GetRect()))
            {
                door1.Open();
            }
            if (CheckCollisionPointRec(GetMousePosition(), door2.GetRect()))
            {
                door2.Open();
            }
            if (CheckCollisionPointRec(GetMousePosition(), door3.GetRect()))
            {
                door3.Open();
            }


        }

        public void Render()
        {
            DrawText("The Monty Hal Game : try your luck and earn a car", 0, 30, 30, Color.RED);
            door1.Draw();
            door2.Draw();
            door3.Draw();
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

        private const int door_width = 100;  // Adjust this value based on your door texture size
        private const int space_between_doors = 20;  // Adjust this value based on the desired space between doors
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            InitWindow(800, 600, "Monty Hal");
            SetTargetFPS(60);
            Door.Init();
            MontyGame game = new();
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