import sys
import urllib
import re

def getSize():
    f = open('clanSize.txt', 'w')
    
    htmlfile = urllib.urlopen("http://www.runeclan.com/clan/The_paradigm_shift")
    
    htmltext = htmlfile.read()
    
    regex = '</div><span class="clan_subtext">Clan Members:</span>(.+?)<br />'

    pattern = re.compile(regex)

    for match in re.findall(pattern, htmltext):
        f.write(match)


        
    f.close()
    
getSize()

