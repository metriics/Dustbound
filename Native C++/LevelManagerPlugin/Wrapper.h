#pragma once
#ifndef __WRAPPER__
#define __WRAPPER__

#include "PluginSettings.h"
#include "LevelManager.h"

#ifdef __cplusplus
extern "C" {
#endif

	// my functions here
	PLUGIN_API void SetSlotID(int slotID);
	PLUGIN_API void AddToObjectList(RawGameObject obj);
	PLUGIN_API RawGameObject GetObjectFromList(int index);
	PLUGIN_API int GetObjectCount();
	PLUGIN_API void ClearList();
	PLUGIN_API bool SaveFile();
	PLUGIN_API bool LoadFile();

#ifdef __cplusplus
}
#endif

#endif /* defined(__WRAPPER__) */