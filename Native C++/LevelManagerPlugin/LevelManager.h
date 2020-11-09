#pragma once
#ifndef __LEVELMANAGER__
#define __LEVELMANAGER__

#include <vector>
#include <string>
#include "PluginSettings.h"
#include "RawGameObject.h"

class PLUGIN_API LevelManager {
public:
	void SetSlotID(int slotID);

	void AddToObjectList(RawGameObject obj);
	RawGameObject GetObjectFromList(int index);

	int GetObjectCount();

	void ClearList();

	bool SaveFile();
	bool LoadFile();

	RawGameObject RawFromString(std::string line);
	int FindNextNewline(std::string string, int prevNewline);

private:
	int m_slotID;
	std::vector<RawGameObject> m_objectList;
};

#endif /* defined(__LEVELMANAGER__) */