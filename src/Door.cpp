#include <Door.hpp>
#include <charconv>
#include <iostream>
#include <string>
#include <string_view>

Texture2D Door::doorTex = Texture2D();
Texture2D Door::zonkTex = Texture2D();
Texture2D Door::carTex = Texture2D();


Door::Door(unsigned int index, DoorStorage storage, DoorState state)
{
    this->id = index;
    this->storage = storage;
    this->state = state;

    if (state == STATE_CLOSED)
    {
        self.width = doorTex.width;
        self.height = doorTex.height;
    }
    else {
        self.width = zonkTex.width;
        self.height = zonkTex.height;
    }
}

void Door::Init()
{
    carTex = LoadTexture("assets/door_car.png");
    zonkTex = LoadTexture("assets/door_zonk.png");
    doorTex = LoadTexture("assets/door.png");
}

void Door::DrawDoorAndNumber()
{
    DrawTexture(doorTex, self.x, self.y, WHITE);
    DrawText(std::to_string(this->id).c_str(), self.x + self.width/2, self.y + 20, 15, BLACK);
}

void Door::Open()
{
    this->state = STATE_OPENED;
}

void Door::Close()
{
    this->state = STATE_CLOSED;
}

int Door::GetWidth()
{
    return this->state == STATE_CLOSED ? doorTex.width : zonkTex.width;
}

int Door::GetHeight()
{
    return this->state == STATE_CLOSED ? doorTex.height : zonkTex.height;
}

void Door::Draw()
{
    switch (state)
    {
        case STATE_CLOSED:
            DrawDoorAndNumber();
            break;

        case STATE_OPENED:
            DrawTexture(this->storage == STORAGE_ZONK ? zonkTex : doorTex, self.x, self.y, WHITE);
            break;
    }
}