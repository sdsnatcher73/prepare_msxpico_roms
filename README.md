<b>Introduction</b>

This little tool can be used to create a large binary file from a directory with ROM files, that can be imported into the MSXpico (in a future firmware update).

The name of the ROM file will be parsed to determine the mapper used by the ROM and the minimum MSX generation it requires.

The naming convention for the ROM files is:

<code>"Name that will appear in the MSX pico menu_mapper_generation.rom"</code>

<b>Examples</b>

<code>A1 Spirit (1987)_konami5_msx1.rom</code><p>
<code>Akumajyo Drakyula - Vampire Killer (1986)_konami4_msx2.rom</code><p>
<code>Athletic Land (1984)_plain4000_msx1.rom</code><p>

<b>Mapper types</b>
<table>
  <tr>
    <td>generic8</td>
    <td>Generic switch, 8kB pages</td>
  </tr>
  <tr>
    <td>generic16</td>
    <td>Generic switch, 16kB pages</td>
  </tr>
  <tr>
    <td>konami5</td>
    <td>Konami 5000/7000/9000/B000h</td>
  </tr>
  <tr>
    <td>konami4</td>
    <td>Konami 4000/6000/8000/A000h</td>
  </tr>
  <tr>
    <td>ascii8</td>
    <td>ASCII 6000/6800/7000/7800h</td>
  </tr>
  <tr>
    <td>ascii16</td>
    <td>ASCII 6000/7000h</td>
  </tr>
  <tr>
    <td>gamemaster2</td>
    <td>onami Game Master 2</td>
  </tr>
  <tr>
    <td>ascii16ex</td>
    <td>ASCII16 with 16 bits mapper</td>
  </tr>
  <tr>
    <td>rtype</td>
    <td>R-Type mapper</td>
  </tr>
  <tr>
    <td>dsk2rom</td>
    <td>Modified DSK2ROM</td>
  </tr>
  <tr>
    <td>plain0000</td>
    <td>Plain ROM starting at 0000h</td>
  </tr>
  <tr>
    <td>plain4000</td>
    <td>Plain ROM starting at 4000h</td>
  </tr>
  <tr>
    <td>plain8000</td>
    <td>Plain ROM starting at 8000h</td>
  </tr>
  <tr>
    <td>neo16</td>
    <td>Neo (Aoineko) 16kB pages</td>
  </tr>
  <tr>
    <td>neo8</td>
    <td>Neo (Aoineko) 8kB pages</td>
  </tr>
  <tr>
    <td></td>
    <td></td>
  </tr>
</table>

<b>Generation types</b>
<table>
  <tr>
    <td>msx1</td>
    <td>MSX1</td>
  </tr>
  <tr>
    <td>msx2</td>
    <td>MSX2</td>
  </tr>
</table>
