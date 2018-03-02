package ch.hearc.structures;

import java.awt.Graphics;

import ch.hearc.interfaces.IObjectPaint;

public class StructureSector implements IObjectPaint {

	private String name;
	
	public StructureSector(String _name) {
		this.name = _name;
	}
	
	public void paint(Graphics _g) {
		_g.drawRect(1, 1, 40, 28);
		_g.drawString(name, 3, 3);
	}
}
