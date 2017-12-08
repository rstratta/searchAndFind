package uy.com.searchandfind.service.cloud.message;

import java.util.concurrent.atomic.AtomicInteger;

/**
 * Created by strat on 30/09/2017.
 */

public class NotificationIdProvider {

    private final static AtomicInteger c = new AtomicInteger(0);
    public static int getID() {
        return c.incrementAndGet();
    }
}
