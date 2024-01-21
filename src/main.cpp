#define RAYGUI_IMPLEMENTATION
#include <raylib.h>
#include <raylib.h>
#include <Door.hpp>
#include <MontyGame.hpp>
int main()
{
    InitWindow(800,600, "Monty Hal Game");
    SetTargetFPS(60);
    MontyGame game;
    game.Init();
    while (!WindowShouldClose())
    {
        game.Update();
        BeginDrawing();
        ClearBackground(WHITE);
        game.Render();
        EndDrawing();
    }
    CloseWindow();
    return 0;
}