import sys
import urllib
import re
import ghost


def getTime():

    link = 'http://www.runescape.wikia.com/wiki/Wilderness_Warbands'

    f = open('warbandTimer.txt', 'w')

    htmlfile = urllib.urlopen(link)

    htmltext = htmlfile.read()


    g = ghost.Ghost()
    with g.start() as session:
        page, extra_resources = session.open("http://www.runescape.wikia.com/wiki/Wilderness_Warbands")
    
        page, result = ghost.evaluate(
        "document.getElementByID('wb-countdown').getAttribute('value');")
        print result
        f.write(result)

   # regex = '<td style="font-size:200%" id="wb-countdown">(.+?)</td>'

    #pattern = re.compile(regex)


    #for match in re.findall(pattern, htmltext):
     #   print match
      #  f.write(value)

    f.close()


getTime()


