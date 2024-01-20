#include <raylib.h>
#include <raygui.h>
#include <Door.hpp>

int main()
{
    InitWindow(800,600, "Monty Hal Game");
    SetTargetFPS(60);
    Door::Init();
    
    Door d(1, STORAGE_ZONK);
    d.self.x = 200;
    d.self.y = 200;

    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(WHITE);
        d.Draw();
        EndDrawing();
    }
    CloseWindow();
    return 0;
}