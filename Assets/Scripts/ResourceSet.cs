using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSet
{
	public ResourceSet(Resource resource, int count)
	{
		Resource = resource;
		Count = count;
	}

	public Resource Resource { get; }

	public int Count { get; }
}
