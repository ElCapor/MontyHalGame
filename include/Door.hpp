/*
The implementation of abstract class Door to represent a door
*/
#ifndef DOOR_HPP
#define DOOR_HPP
#include <Enums.hpp>
#include <raylib.h>

class Door
{
// self data
public:
    unsigned int id; // door id
    Rectangle self;
    DoorState state;
    DoorStorage storage;
// common data
public:
    static Texture2D carTex;
    static Texture2D zonkTex;
    static Texture2D doorTex;

    static void Init();

public:
    Door(unsigned int index, DoorStorage storage, DoorState state = DoorState::STATE_CLOSED);

    // Draw the number of the current door on the texture
    void DrawDoorAndNumber();

    // simply draw the door
    void Draw();

    // open the door
    void Open();

    // close it
    void Close();

    // get width & height
    int GetWidth();
    int GetHeight();
};  

#endif