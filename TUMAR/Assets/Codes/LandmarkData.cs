using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct DataCell
{
    public int id;
    public int type;
    public string name;
    public string realName;
    public string description;
    public List<int> connectedIDs;
    public Color? color;
}

// TODO: add color attribute to DataCell for all primary objects; primary objects should be set in each area.
// Meanwhile, the color of the corresponding secondary objects should be set as half saturation. We preset
// some object types and assign them with different colors (mainly for office objects, which are full of 
// senmantic information). Highlight all related objects if the primary object is being watched.

/*************************************************************

PINK: (1f, 0.5f, 0.5f, 1f), fire extinguisher
ORANGE: (1f, 0.5f, 0f, 1f), garbage can
SHALLOW BLUE: (0f, 0.5f, 1f, 1f), show board
GREEN: (0f, 1f, 0f, 1f), stereoscope
YELLOW: (1f, 1f, 0f, 1f), toilet

*************************************************************/
public class LandmarkData
{
    public List<DataCell> objs = new List<DataCell>();
    public LandmarkData()
    {
        // Area D and G should be abondoned


        // Area A: corner area of the U route.
        objs.Add(new DataCell
        {
            id = 1,
            type = 2,
            name = "GlassDoor_A-B",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 2,
            type = 2,
            name = "GlassDoor_A-E",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 3,
            type = 0,
            name = "Room_Door_1757",
            realName = "Room 1757",
            description = "Connected Transport Systems, Prof. Antoniou",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 4,
            type = 0,
            name = "Room_Door_1755",
            realName = "Room 1755",
            description = "Chair of Traffic Engineering, Prof. Bogenberger",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 5,
            type = 0,
            name = "Restroom1",
            realName = "Room 1751",
            description = "Toilet",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 6,
            type = 0,
            name = "show6",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int> { 7 }
        });
        objs.Add(new DataCell
        {
            id = 7,
            type = 1,
            name = "show7",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        // Area B: long route for Cartography
        objs.Add(new DataCell
        {
            id = 8,
            type = 0,
            name = "Room_Door_1761",
            realName = "Room 1761",
            description = "Hydrology & River Basin Management, Prof. Broich",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 9,
            type = 0,
            name = "Room_Door_1762",
            realName = "Room 1762",
            description = "Transportation Construction",
            connectedIDs = new List<int> { 10, 11, 12, 13 }
        });
        objs.Add(new DataCell
        {
            id = 10,
            type = 1,
            name = "Room_Door_1763",
            realName = "Room 1763",
            description = "TC Secretariat",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 11,
            type = 1,
            name = "Room_Door_1766",
            realName = "Room 1766",
            description = "TC Library",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 12,
            type = 1,
            name = "Room_Door_1765",
            realName = "Room 1765",
            description = "TC Prof. Freudenstein",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 13,
            type = 1,
            name = "Room_Door_1768",
            realName = "Room 1768",
            description = "TC Dr. Lechner, Msc. Lillin",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 14,
            type = 1,
            name = "Room_Door_1767",
            realName = "Room 1767",
            description = "Carto Library",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 15,
            type = 0,
            name = "Room_Door_1770",
            realName = "Room 1770",
            description = "Photogrammetry & Remote Sensing, workshop",
            connectedIDs = new List<int> { 17, 19, 21 }
        });
        objs.Add(new DataCell
        {
            id = 16,
            type = 1,
            name = "Room_Door_1769",
            realName = "Room 1769",
            description = "Carto Msc. office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 17,
            type = 1,
            name = "Room_Door_1772",
            realName = "Room 1772",
            description = "PR Msc. office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 18,
            type = 1,
            name = "Room_Door_1771",
            realName = "Room 1771",
            description = "Carto Dr. office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 19,
            type = 1,
            name = "Room_Door_1774",
            realName = "Room 1774",
            description = "PR Msc. office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 20,
            type = 0,
            name = "Room_Door_1775",
            realName = "Room 1775",
            description = "Cartography Prof. Meng",
            connectedIDs = new List<int> { 14, 16, 18, 22, 44 }
        });
        objs.Add(new DataCell
        {
            id = 21,
            type = 1,
            name = "Room_Door_1776",
            realName = "Room 1776",
            description = "PR Msc. office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 22,
            type = 1,
            name = "Room_Door_1777",
            realName = "Room 1777",
            description = "Cartography Prof. Meng",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 23,
            type = 0,
            name = "Room_Door_1784",
            realName = "Room 1784",
            description = "LRZ node.",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 24,
            type = 0,
            name = "show8",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int> { 25, 26, 27 }
        });
        objs.Add(new DataCell
        {
            id = 25,
            type = 1,
            name = "show9",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 26,
            type = 1,
            name = "show10",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 27,
            type = 1,
            name = "show11",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 28,
            type = 0,
            name = "fireExitinguisher3",
            realName = "Fire extinguisher",
            description = "Fire extinguisher",
            connectedIDs = new List<int> { 29 }
        });
        objs.Add(new DataCell
        {
            id = 29,
            type = 1,
            name = "fireExitinguisher4",
            realName = "Fire extinguisher",
            description = "Fire extinguisher",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 30,
            type = 0,
            name = "stereoScope1",
            realName = "Stereo scope",
            description = "Stereo scope",
            connectedIDs = new List<int>()
        });
        // Area C: Cartography coordinator office direction
        objs.Add(new DataCell
        {
            id = 31,
            type = 2,
            name = "GlassDoor_C-B",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 32,
            type = 2,
            name = "GlassDoor_C-D",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 33,
            type = 0,
            name = "fireExitinguisher5",
            realName = "Fire extinguisher",
            description = "Fire extinguisher",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 34,
            type = 0,
            name = "stereoScope2",
            realName = "Stereo scope",
            description = "Stereo scope",
            connectedIDs = new List<int> { 35, 36, 37, 38 }
        });
        objs.Add(new DataCell
        {
            id = 35,
            type = 1,
            name = "stereoScope3",
            realName = "Stereo scope",
            description = "Stereo scope",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 36,
            type = 1,
            name = "stereoScope4",
            realName = "Stereo scope",
            description = "Stereo scope",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 37,
            type = 1,
            name = "stereoScope5",
            realName = "Stereo scope",
            description = "Stereo scope",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 38,
            type = 1,
            name = "stereoScope6",
            realName = "Stereo scope",
            description = "Stereo scope",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 39,
            type = 0,
            name = "show12",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 40,
            type = 0,
            name = "board1",
            realName = "Board",
            description = "Board",
            connectedIDs = new List<int> { 41, 42 }
        });
        objs.Add(new DataCell
        {
            id = 41,
            type = 1,
            name = "board2",
            realName = "Board",
            description = "Board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 42,
            type = 1,
            name = "board3",
            realName = "Board",
            description = "Board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 43,
            type = 0,
            name = "model1",
            realName = "Model",
            description = "Model",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 44,
            type = 1,
            name = "Room_Door_1796",
            realName = "Room 1796",
            description = "Carto Coodinator Office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 45, 
            type = 1, 
            name = "Room_Door_1778", 
            realName = "Room 1778", 
            description = "PR. Computer Lab", 
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 46,
            type = 1,
            name = "Room_Door_1780",
            realName = "Room 1780",
            description = "PR. Dr. Office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 47,
            type = 1,
            name = "Room_Door_1782",
            realName = "Room 1782",
            description = "SignalProcessing & EarthObservation Prof Office",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 48,
            type = 0,
            name = "Room_Door_1783",
            realName = "Room 1783",
            description = "Photogrammetry & Remote Sensing Prof Office",
            connectedIDs = new List<int> { 45, 46, 47, 49 }
        });
        objs.Add(new DataCell
        {
            id = 49,
            type = 1,
            name = "Room_Door_1789",
            realName = "Room 1789",
            description = "PR, Prof. Bamler, Prof. Zhu",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 50,
            type = 0,
            name = "Room_Door_1795A",
            realName = "Room 1795 A",
            description = "Emergency balcony.",
            connectedIDs = new List<int> { 51 }
        });
        objs.Add(new DataCell
        {
            id = 51,
            type = 1,
            name = "Room_Door_1795B",
            realName = "Room 1795 B",
            description = "Office.",
            connectedIDs = new List<int>()
        });
        // Area D: abondoned
        objs.Add(new DataCell
        {
            id = 52,
            type = 2,
            name = "GlassDoor_D-X",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 53,
            type = 0,
            name = "LectureHall",
            realName = "Lecture Hall",
            description = "Lecture Hall",
            connectedIDs = new List<int> { 54, 55 }
        });
        objs.Add(new DataCell
        {
            id = 54,
            type = 1,
            name = "Room_1112",
            realName = "Room 1112",
            description = "Office.",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 55,
            type = 1,
            name = "Lifts",
            realName = "Lift",
            description = "Lift",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 56,
            type = 0,
            name = "garbage2",
            realName = "Garbage Can",
            description = "Garbage Can",
            connectedIDs = new List<int>()
        });
        // Area E: small part beside the corner
        objs.Add(new DataCell
        {
            id = 57,
            type = 2,
            name = "GlassDoor_E-A",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        
        objs.Add(new DataCell
        {
            id = 58,
            type = 2,
            name = "GlassDoor_E-F",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 59,
            type = 1,
            name = "Room_Door_1749",
            realName = "Room 1749",
            description = "Toilet Female",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 60,
            type = 1,
            name = "Room_Door_1743",
            realName = "Room 1743",
            description = "OpenLAB Dr. Wulfhorst",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 61,
            type = 1,
            name = "garbage1",
            realName = "Garbage Can",
            description = "Garbage Can",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 62,
            type = 0,
            name = "show4",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int> { 63 }
        });
        objs.Add(new DataCell
        {
            id = 63,
            type = 1,
            name = "show5",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        // Area F: City Lab area
        objs.Add(new DataCell
        {
            id = 64,
            type = 0,
            name = "Room_Door_1734",
            realName = "Room 1734",
            description = "OpenLab City Mobility",
            connectedIDs = new List<int> { 60, 65, 66 }
        });
        objs.Add(new DataCell
        {
            id = 65,
            type = 1,
            name = "Room_Door_1731",
            realName = "Room 1731",
            description = "OpenLab City Mobility",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 66,
            type = 1,
            name = "Room_Door_1736",
            realName = "Room 1736",
            description = "Settlement Structure and Transport Planning, Prof. Wulfhorst.",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 67,
            type = 2,
            name = "GlassDoor_F-G",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 68,
            type = 0,
            name = "show1",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int> { 69, 70 }
        });
        objs.Add(new DataCell
        {
            id = 69,
            type = 1,
            name = "show2",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 70,
            type = 1,
            name = "show3",
            realName = "Show board",
            description = "Show board",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 71,
            type = 0,
            name = "fireExitinguisher1",
            realName = "Fire extinguisher",
            description = "Fire extinguisher",
            connectedIDs = new List<int> { 72 }
        });
        objs.Add(new DataCell
        {
            id = 72,
            type = 1,
            name = "fireExitinguisher2",
            realName = "Fire extinguisher",
            description = "Fire extinguisher",
            connectedIDs = new List<int>()
        });
        // Area G: abondoned
        objs.Add(new DataCell
        {
            id = 73,
            type = 2,
            name = "GlassDoor_G-X",
            realName = "Glass door",
            description = "Glass door",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 74,
            type = 0,
            name = "Room_Door_1715",
            realName = "Room 1715",
            description = "Office.",
            connectedIDs = new List<int> { 75 }
        });
        objs.Add(new DataCell
        {
            id = 75,
            type = 1,
            name = "Room_Door_1717",
            realName = "Room 1717",
            description = "Hydrology & River Basin Management, Prof. Disse.",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 76,
            type = 0,
            name = "cabinet1",
            realName = "Cabinet",
            description = "Cabinet",
            connectedIDs = new List<int> { 77 }
        });
        objs.Add(new DataCell
        {
            id = 77,
            type = 1,
            name = "cabinet2",
            realName = "Cabinet",
            description = "Cabinet",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 78,
            type = 0,
            name = "fireExitinguisher6",
            realName = "Fire extinguisher",
            description = "Fire extinguisher",
            connectedIDs = new List<int>()
        });
        objs.Add(new DataCell
        {
            id = 79,
            type = 0,
            name = "fireExitinguisher7",
            realName = "Fire extinguisher",
            description = "Fire extinguisher",
            connectedIDs = new List<int>()
        });

    }
}