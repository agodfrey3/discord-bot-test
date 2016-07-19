import sys
import urllib
import re

def getPath():

    link = 'http://runescape.wikia.com/wiki/Araxxor'
    
    f = open('path1.txt', 'w')
    g = open('path2.txt', 'w')
    a = open('path3.txt', 'w')
    
    htmlfile = urllib.urlopen(link)
    
    htmltext = htmlfile.read()
    
    regex = '<tr><td class=(.+?)</td></tr>'

    pattern = re.compile(regex)

    for match in re.findall(pattern, htmltext):
        if (match[8] == 'i'):
            f.write('closed')
        elif(match[8] == 'a'):
            f.write('open')
        
        if (match[47] == 'i'):
            g.write('closed')
        elif(match[47] == 'a'):
           g.write('open')
        elif(match[45] == 'i'):
            g.write('closed')
        elif(match[45] == 'a'):
            g.write('open')
        
        if(match[82] == 'i'):
            a.write('closed')
        elif(match[82] == 'a'):
            a.write('open')
        elif(match[78] == 'i'):
            a.write('closed')
        elif(match[78] == 'a'):
            a.write('open')
    f.close()

getPath()