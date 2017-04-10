using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using DG.Tweening;

public class MapController : GenericSingleton<MapController> {

    // points where robots are
    [SerializeField]
    private Transform[] route_points_;

    // the avatar of the player
    [SerializeField]
    private GameObject sprite_;

    // this is index of the level recorded in the map
    private static int map_level_index_ = 0;

    void Start() {
        if (sprite_ != null && route_points_.Length > 0)
            sprite_.transform.position = route_points_[0].position;

        int level_index = GameController.GetCurrentLevel();
        if (map_level_index_ != level_index)
            TransFromMapLevelToControllerLevel(level_index);
    }

    // if the level index in level controller is different from the level recorded in the map
    // then we should start some animation in the map and unlock the later level until their
    // indexes are same.
    private void TransFromMapLevelToControllerLevel(int level_index) {
        Debug.Assert(map_level_index_ != level_index);
        int step = map_level_index_ < level_index ? 1 : -1; // NP-Hard

        if (level_index < route_points_.Length) {
            map_level_index_ += step;
            sprite_.transform.DOMove(route_points_[map_level_index_].position, 1.0f);
        }
    }
}