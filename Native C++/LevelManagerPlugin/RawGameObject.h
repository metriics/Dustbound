#pragma once
#ifndef __RAWGAMEOBJECT__
#define __RAWGAMEOBJECT__

struct RawGameObject {
	int typeID;
	int matID;
	float xPos, yPos, zPos;
	float xRot, yRot, zRot, wRot;
	float xScl, yScl, zScl;
};

#endif /* defined(__RAWGAMEOBJECT__) */