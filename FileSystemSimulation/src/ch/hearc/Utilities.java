package ch.hearc;

import java.io.IOException;
import java.util.Properties;
import java.util.regex.PatternSyntaxException;

public class Utilities {

	public static String getExtensionName(String _path) {
		String extension = null;
		
		int index = _path.lastIndexOf(".");
		if(index > 0) {
			extension = _path.substring(index + 1);
		}
		return extension;
	}
	
	public static String[] getPropertiesArray(Properties _properties, String _propertyName, String _split) throws IOException, PatternSyntaxException {
		return _properties.getProperty(_propertyName).split(_split);
	}
}
