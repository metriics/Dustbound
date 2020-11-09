#include "Wrapper.h"

LevelManager manager;

PLUGIN_API void SetSlotID(int slotID) {
	return manager.SetSlotID(slotID);
}

PLUGIN_API void AddToObjectList(RawGameObject obj) {
	return manager.AddToObjectList(obj);
}

PLUGIN_API RawGameObject GetObjectFromList(int index) {
	return manager.GetObjectFromList(index);
}

PLUGIN_API int GetObjectCount() {
	return manager.GetObjectCount();
}

PLUGIN_API void ClearList() {
	return manager.ClearList();
}

PLUGIN_API bool SaveFile() {
	return manager.SaveFile();
}

PLUGIN_API bool LoadFile() {
	return manager.LoadFile();
}
