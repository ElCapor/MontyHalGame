// old code i took from my repo :
// https://raw.githubusercontent.com/ElCapor/FunnyPlatformerTest89247/master/FunnyPlatformerTest89247/Core/GUI/FlexRect.hpp
#ifndef FLEXRECT_HPP
#define FLEXREC_HPP
#include <raylib.h>
/*
An attempt at writing a flex rect system for the ui stuff
https://github.com/nezvers/GameSystemsInC/blob/master/FlexRect/flexrect.h
*/
class FlexRect {
public:
	FlexRect()
	{
		// empty constructor
	}
	FlexRect(Rectangle rect, float al, float at, float ar, float ab, int ml, int mt, int mr, int mb, int minw, int minh)
		: rect(rect), al(al), at(at), ar(ar), ab(ab), ml(ml), mt(mt), mr(mr), mb(mb), minw(minw), minh(minh), childCount(0), children(nullptr), parent(nullptr) {}

	~FlexRect() {
		for (int i = 0; i < childCount; i++) {
			delete children[i];
		}
		delete[] children;
	}

	FlexRect* CreateChild(float al, float at, float ar, float ab, int ml, int mt, int mr, int mb, int minw, int minh) {
		FlexRect* newChild = new FlexRect(FlexRectGetRect(rect, al, at, ar, ab, ml, mt, mr, mb, minw, minh), al, at, ar, ab, ml, mt, mr, mb, minw, minh);
		newChild->parent = this;

		FlexRect** old = children;
		children = new FlexRect * [childCount + 1];

		for (int i = 0; i < childCount; i++) {
			children[i] = old[i];
		}

		children[childCount] = newChild;

		if (childCount > 0) {
			delete[] old;
		}

		childCount++;

		return newChild;
	}

	void RemoveChild(FlexRect* child) {
		if (childCount < 1) {
			return;
		}
		else if (childCount == 1) {
			delete[] children;
			childCount = 0;
			children = nullptr;
			return;
		}

		FlexRect** old = children;
		children = new FlexRect * [childCount - 1];

		for (int i = 0, j = 0; i < childCount; i++) {
			if (old[i] != child) {
				children[j++] = old[i];
			}
		}

		delete[] old;
		childCount--;
	}

	void Resize(Rectangle newRect) {
		rect = FlexRectGetRect(newRect, al, at, ar, ab, ml, mt, mr, mb, minw, minh);
		for (int i = 0; i < childCount; i++) {
			children[i]->Resize(rect);
		}
	}

	void DrawBorder()
	{
		DrawRectangleLines(rect.x, rect.y, rect.width, rect.height, RED);
	}

	Rectangle GetRect()
	{
		return rect;
	}

	Rectangle GetMargin()
	{
		return {
			(float)ml, (float)mt, (float)mr, (float)mb
		};
	}
private:
	Rectangle rect;
	float al, at, ar, ab;
	int ml, mt, mr, mb;
	int minw, minh;
	int childCount;
	FlexRect** children;
	FlexRect* parent;

	Rectangle FlexRectGetRect(Rectangle rect, float al, float at, float ar, float ab, int ml, int mt, int mr, int mb, int minw, int minh) {
		int x = static_cast<int>(rect.x + (rect.width * al) + ml);
		int y = static_cast<int>(rect.y + (rect.height * at) + mt);
		int w = static_cast<int>((rect.width - (rect.width * al) - ml - mr) * ar);
		int h = static_cast<int>((rect.height - (rect.height * at) - mt - mb) * ab);

		if (w < minw) {
			w = minw;
		}
		if (h < minh) {
			h = minh;
		}

		return { static_cast<float>(x), static_cast<float>(y), static_cast<float>(w), static_cast<float>(h) };
	}
};


#endif // !FLEXRECT_HPP