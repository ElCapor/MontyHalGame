/*
Monty Game main class
*/
#ifndef MONTYGAME_HPP
#define MONTYGAME_HPP
#include <Door.hpp>
#include <raygui.h>
class MontyGame
{
public:
    GameState m_GameState; // state of the game
    Texture2D backgroundTex; // TODO : Make a resource manager class (singleton)
    Texture2D titleTex;
public:
    MontyGame(); // constructor

    // init all game data
    void Init();

    // update data
    void Update();

    // Render Game
    void Render();

    // change the game state
    void SetState(GameState newState);
};
#endif