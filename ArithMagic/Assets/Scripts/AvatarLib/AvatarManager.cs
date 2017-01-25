using AvatarLib;

public class AvatarManager {

    private static AvatarProfile curr_profile;
    private static AvatarBackEnd backend;

    public AvatarManager() {
        backend = new AvatarBackEnd(false);
        curr_profile = null;
    }
}
