#include <MontyGame.hpp>
#include <FlexRect.hpp>
#include <iostream>
MontyGame::MontyGame()
{
    this->m_GameState = GameState::GameMainMenu;
}

FlexRect windowFlex;
FlexRect* titleFlex;

FlexRect* buttonsFlex;
FlexRect* playBtnFlex;

void MontyGame::Init()
{
    Door::Init();
    #pragma region gui_init
    Rectangle parentRect = {
        0,0,GetScreenWidth(), GetScreenHeight()
    };
    windowFlex = FlexRect(parentRect, 0,0,1,1,10,10,10,10, 0,0);
    titleFlex = windowFlex.CreateChild(0,0,1,0.15,10,10,10,10,0,0);

    buttonsFlex = windowFlex.CreateChild(0.20,0.35, 0.75, 0.75, 10, 10, 10, 10, 0,0);
    playBtnFlex = buttonsFlex->CreateChild(0,0,1,0.3, 60,30,60,30, 0,0);

    #pragma endregion gui_init
    #pragma region bg_loading
    // Loading the Background
    Image bg = LoadImage("assets/background.png");
    float scaleX = (float)GetScreenWidth() / bg.width;
    float scaleY = (float)GetScreenHeight() / bg.height;
    ImageResize(&bg, (int)(bg.width * scaleX), (int)(bg.height * scaleY));
    backgroundTex = LoadTextureFromImage(bg);
    UnloadImage(bg);

    titleTex = LoadTexture("assets/logo.png");
    #pragma endregion bg_loading
    
}

void MontyGame::Update()
{
    switch (this->m_GameState)
    {
        case GameMainMenu:
            break;
    }
}

void MontyGame::Render()
{
    switch (this->m_GameState)
    {
        case GameMainMenu:
            {
                DrawTexture(backgroundTex, 0, 0, WHITE);
                DrawTexturePro(titleTex, {0, 0, (float)titleTex.width, (float)titleTex.height},titleFlex->GetRect(), {0,0},0, WHITE );
                windowFlex.DrawBorder();
                titleFlex->DrawBorder();
                buttonsFlex->DrawBorder();
                playBtnFlex->DrawBorder();

                if (GuiButton(playBtnFlex->GetRect(), "Play"))
                {
                    printf("Test");
                }
            }
            break;
    }
}

void MontyGame::SetState(GameState newState)
{
    this->m_GameState = newState;
}
