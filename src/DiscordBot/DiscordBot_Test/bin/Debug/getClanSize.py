import sys
import urllib
import re

def getSize():
    f = open('clanSize.txt', 'w')
    
    htmlfile = urllib.urlopen("http://services.runescape.com/m=clan-home/clan/The%20paradigm%20shift")
    
    htmltext = htmlfile.read()
    
    regex = '<span class="clanstatTitle">Total Members:</span><span class="clanstatVal FlatHeader">(.+?)</span>'

    pattern = re.compile(regex)

    for match in re.findall(pattern, htmltext):
        f.write(match)


        
    f.close()
    
getSize()

