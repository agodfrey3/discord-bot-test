import sys
import urllib
import re
from itertools import islice

def getPrice():
    with open('item.txt') as lines:
        for line in islice(lines, 0, 1):
            item = line

    link = 'http://runescape.wikia.com/wiki/' + item
    
    f = open('itemPrice.txt', 'w')
    
    htmlfile = urllib.urlopen(link)
    
    htmltext = htmlfile.read()
    
    regex = 'class="infobox-quantity-replace">(.+?)</span>'

    pattern = re.compile(regex)

    i = 0


    for match in re.findall(pattern, htmltext):
        if(match):
            if(i == 0):
                i = i + 1            
                f.write(match)
        
    f.close()
    

getPrice()