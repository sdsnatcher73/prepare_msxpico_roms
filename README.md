This little tool can be used to create a large binary file from a directory with ROM files, that can be imported into the MSXpico (in a future firmware update).

The name of the ROM file will be parsed to determine the mapper used by the ROM and the minimum MSX generation it requires.

The naming convention for the ROM files is:

"Name that will appear in thr MSX pico menu_mapper_generation.rom"

Examples:

A1 Spirit (1987)_konami5_msx1.rom
Akumajyo Drakyula - Vampire Killer (1986)_konami4_msx2.rom
Athletic Land (1984)_plain4000_msx1.rom

The possible values for mapper and generation are:

Mapper types:
generic8        Generic switch, 8kB pages
generic16       Generic switch, 16kB pages
konami5         Konami 5000/7000/9000/B000h
konami4         Konami 4000/6000/8000/A000h
ascii8          ASCII 6000/6800/7000/7800h
ascii16         ASCII 6000/7000h
gamemaster2     Konami GameMaster2
ascii16ex       ASCII16 with 16 bits mapper
rtype           R-Type mapper
dsk2rom         Modified DSK2ROM
plain0000       Plain ROM starting at 0000h
plain4000       Plain ROM starting at 4000h
plain8000       Plain ROM starting at 8000h
neo16           Neo (Aoineko) 16kB pages
neo8            Neo (Aoineko) 8kB pages
konamiultimate  Konami Ultimate Collection mapper

Generation types
msx1            MSX1
msx2            MSX2
