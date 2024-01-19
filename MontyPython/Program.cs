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
        None,
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
    }


    class MontyGame
    {
        Door door1;
        Door door2;
        Door door3;


        bool opened_first;
        bool user_choose;
        bool isWin;

        public MontyGame() { }

        public void Init()
        {

        }

        public void Update()
        {

        }

        public void Render()
        {

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            InitWindow(800, 600, "Monty Hal");
            SetTargetFPS(60);
            Door.Init();
            Door door1 = new(1, DoorState.CLOSED, Storage.Zonk);
            door1.position = new Vector2(200, 300);
            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.WHITE);
                door1.Draw();
                EndDrawing();
            }

            CloseWindow();
        }
    }
}