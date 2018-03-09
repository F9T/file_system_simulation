package ch.hearc.interfaces;

import java.awt.Graphics;

public interface IObjectPaint {
	public void paint(Graphics _g);
	public boolean intersects(int _x, int _y);
}
