using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardTile : MonoBehaviour {
    // Public Attributes
    public double positionX;
    public double positionY;
    /* isOccupied - determines if a board tile is occupied
     * @Args
     *      posX  - x value of the tile's position
     *      posY  - y value of the tile's position
     * @return
     *      1 if occupied, -1 if not
     */
    private int isOccupied(double posX, double posY)
    {
        int ret = -1;

        return ret;
    }
    /* isBorderedOn - determines if a tile has obstacles adjacent to it
     * @Args
     *     this     checks self borders
     *     
     * @return      returns a <=4 int array [N, S, E, W], 0 = true, -1 = false 
     */
    private int[] isBorderedOn()
    {
        int[] ret = new int[4];

        return ret;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
