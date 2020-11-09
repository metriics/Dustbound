#include "LevelManager.h"
#include <string>
#include <fstream>
#include <streambuf>

#include <iostream>

void LevelManager::SetSlotID(int slotID) {
    m_slotID = slotID;
}

void LevelManager::AddToObjectList(RawGameObject obj) {
    m_objectList.push_back(obj);
}

RawGameObject LevelManager::GetObjectFromList(int index) {
    RawGameObject temp;
    if (index < m_objectList.size()) {
        temp = m_objectList[index];
    }
    return temp;
}

int LevelManager::GetObjectCount() {
    return m_objectList.size();
}

void LevelManager::ClearList() {
    m_objectList.clear();
}

bool LevelManager::SaveFile() {
    std::string listString;
    listString.append(std::to_string(GetObjectCount()) + "\n\n");

    // append every raw object's data to string, seperated by some identifier
    for (int i = 0; i < GetObjectCount(); i++) {
        RawGameObject temp = GetObjectFromList(i);
        listString.append(std::to_string(temp.typeID) + " " + std::to_string(temp.matID) + " " + std::to_string(temp.xPos) + " " + std::to_string(temp.yPos) + " " + std::to_string(temp.zPos) + " " + std::to_string(temp.xRot) + " " + std::to_string(temp.yRot) + " " + std::to_string(temp.zRot) + " " + std::to_string(temp.wRot) + " " + std::to_string(temp.xScl) + " " + std::to_string(temp.yScl) + " " + std::to_string(temp.zScl) + "\n");
    }

    // write to file whose name is the slotID
    std::ofstream out(std::to_string(m_slotID) + ".txt");
    if (out) {
        out << listString;
        out.close();
        return true;
    }
    return false;
}

bool LevelManager::LoadFile() {
    std::ifstream in(std::to_string(m_slotID) + ".txt");
    if (!in) {
        std::cout << "load failure" << std::endl;
        return false;
    }
    std::string listString((std::istreambuf_iterator<char>(in)), std::istreambuf_iterator<char>());
    
    int firstLineEnd = FindNextNewline(listString, 0);
    int reportedListSize = std::stoi(listString.substr(0, firstLineEnd));
    int prevLineEnd = FindNextNewline(listString, firstLineEnd + 1); // skip first two lines, they should never contain object string

    for (int i = 0; i < reportedListSize; i++) {
        int curLineEnd = FindNextNewline(listString, prevLineEnd + 1);
        AddToObjectList(RawFromString(listString.substr(prevLineEnd + 1, curLineEnd - prevLineEnd - 1)));
        prevLineEnd = curLineEnd;
    }

    return true;
}

RawGameObject LevelManager::RawFromString(std::string line) {
    RawGameObject fromLine;
    int prevSpace = 0;
    std::vector<int> spaceIndices;

    for (int i = 0; i < 11; i++) {
        int curSpace = line.find(" ", prevSpace + 1);
        spaceIndices.push_back(curSpace);
        prevSpace = curSpace;
    }
    
    fromLine.typeID = std::stoi(line.substr(0, spaceIndices[0]));
    fromLine.matID = std::stoi(line.substr(spaceIndices[0] + 1, spaceIndices[1] - spaceIndices[0] - 1));
    fromLine.xPos = std::stof(line.substr(spaceIndices[1] + 1, spaceIndices[2] - spaceIndices[1] - 1));
    fromLine.yPos = std::stof(line.substr(spaceIndices[2] + 1, spaceIndices[3] - spaceIndices[2] - 1));
    fromLine.zPos = std::stof(line.substr(spaceIndices[3] + 1, spaceIndices[4] - spaceIndices[3] - 1));
    fromLine.xRot = std::stof(line.substr(spaceIndices[4] + 1, spaceIndices[5] - spaceIndices[4] - 1));
    fromLine.yRot = std::stof(line.substr(spaceIndices[5] + 1, spaceIndices[6] - spaceIndices[5] - 1));
    fromLine.zRot = std::stof(line.substr(spaceIndices[6] + 1, spaceIndices[7] - spaceIndices[6] - 1));
    fromLine.wRot = std::stof(line.substr(spaceIndices[7] + 1, spaceIndices[8] - spaceIndices[7] - 1));
    fromLine.xScl = std::stof(line.substr(spaceIndices[8] + 1, spaceIndices[9] - spaceIndices[8] - 1));
    fromLine.yScl = std::stof(line.substr(spaceIndices[9] + 1, spaceIndices[10] - spaceIndices[9] - 1));
    fromLine.zScl = std::stof(line.substr(spaceIndices[10] + 1, std::string::npos));

    return fromLine;
}

int LevelManager::FindNextNewline(std::string string, int prevNewline) {
    int normal = string.find('\n', prevNewline);
    int dumb = string.find('\r\n', prevNewline);

    if (normal != -1) {
        return normal;
    }
    else if (dumb != -1) {
        return dumb;
    }
    else {
        return -1;
    }
}
